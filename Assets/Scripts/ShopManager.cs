using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Inspector fields
    // -------------------------------------------------------------------------

    [Header("Inventory")]
    public List<ShopItemData> shopInventory = new();

    [Header("Slot Prefab & Container")]
    public GameObject shopSlotPrefab;          // prefab with ShopSlotUI component
    public Transform itemListParent;           // parent for spawned slots

    [Header("Detail Panel")]
    public TMP_Text detailNameText;
    public TMP_Text detailDescriptionText;
    public TMP_Text detailPriceText;
    public Image detailIconImage;
    public Button confirmBuyButton;
    public GameObject detailPanel;            // hide when nothing is selected

    [Header("Feedback")]
    public TMP_Text feedbackText;             // "Not enough Geo!", "Purchased!" etc.
    public float feedbackDuration = 2f;

    [Header("Shop Root")]
    public GameObject shopRoot;               // the whole shop canvas/panel

    // -------------------------------------------------------------------------
    // Private state
    // -------------------------------------------------------------------------

    private readonly List<ShopSlotUI> _slots = new();
    private ShopItemData _selectedItem;
    private ShopSlotUI _selectedSlot;
    private Coroutine _feedbackCoroutine;

    // -------------------------------------------------------------------------
    // Unity lifecycle
    // -------------------------------------------------------------------------

    private void Awake()
    {
        confirmBuyButton.onClick.AddListener(OnConfirmPurchase);
        CloseShop();
    }

    // -------------------------------------------------------------------------
    // Public API
    // -------------------------------------------------------------------------

    public void OpenShop()
    {
        shopRoot.SetActive(true);
        Time.timeScale = 0f;                  // pause gameplay while shopping
        BuildSlots();
        ClearSelection();
    }

    public void CloseShop()
    {
        shopRoot.SetActive(false);
        Time.timeScale = 1f;
    }

    // -------------------------------------------------------------------------
    // Slot construction
    // -------------------------------------------------------------------------

    private void BuildSlots()
    {
        // Destroy old slots
        foreach (var slot in _slots)
            Destroy(slot.gameObject);
        _slots.Clear();

        // Spawn one slot per inventory item
        foreach (var itemData in shopInventory)
        {
            var go = Instantiate(shopSlotPrefab, itemListParent);
            var slot = go.GetComponent<ShopSlotUI>();
            slot.Initialise(itemData, OnItemSelected);
            _slots.Add(slot);
        }
    }

    // -------------------------------------------------------------------------
    // Selection
    // -------------------------------------------------------------------------

    private void OnItemSelected(ShopItemData data)
    {
        // Deselect previous
        _selectedSlot?.SetSelected(false);

        // Find the slot that owns this data
        _selectedSlot = _slots.Find(s => s.name == data.name)
                        ?? FindSlotForData(data);
        _selectedSlot?.SetSelected(true);

        _selectedItem = data;
        ShowDetail(data);
    }

    private void ShowDetail(ShopItemData data)
    {
        detailPanel.SetActive(true);
        detailNameText.text = data.itemName;
        detailDescriptionText.text = data.description;
        detailPriceText.text = $"{data.price} Geo";
        detailIconImage.sprite = data.icon;

        // Disable buy button if sold out
        confirmBuyButton.interactable = !data.isSoldOut;
    }

    private void ClearSelection()
    {
        _selectedItem = null;
        _selectedSlot?.SetSelected(false);
        _selectedSlot = null;
        detailPanel.SetActive(false);
        HideFeedback();
    }

    // -------------------------------------------------------------------------
    // Purchase
    // -------------------------------------------------------------------------

    private void OnConfirmPurchase()
    {
        if (_selectedItem == null) return;

        // --- Currency check ---
        if (!PlayerWallet.Instance.HasEnough(_selectedItem.price))
        {
            ShowFeedback("Not enough Geo...");
            return;
        }

        // --- Deduct currency ---
        PlayerWallet.Instance.Spend(_selectedItem.price);

        // --- Apply item effect ---
        ApplyItemEffect(_selectedItem);

        // --- Handle sold-out logic ---
        if (!_selectedItem.isConsumable)
        {
            _selectedItem.isSoldOut = true;
            _selectedSlot?.SetSoldOut(true);
            confirmBuyButton.interactable = false;
        }

        ShowFeedback($"Purchased {_selectedItem.itemName}!");
    }

    private void ApplyItemEffect(ShopItemData data)
    {
        // Extend this switch as you add more effect types.
        switch (data.effectType)
        {
            case ItemEffectType.HealHP:
                PlayerHealth.Instance.Heal(data.effectValue);
                break;

            case ItemEffectType.IncreaseMaxHP:
                PlayerHealth.Instance.IncreaseMaxHP(data.effectValue);
                break;

            case ItemEffectType.IncreaseMaxSoul:
                PlayerSoul.Instance.IncreaseMaxSoul(data.effectValue);
                break;

            case ItemEffectType.AddCharm:
                CharmInventory.Instance.Add(data);
                break;

            case ItemEffectType.UnlockAbility:
                AbilityManager.Instance.Unlock(data.itemName);
                break;

            case ItemEffectType.Custom:
                // Fire a Unity event, send a message, etc.
                Debug.Log($"[Shop] Custom effect triggered for: {data.itemName}");
                break;
        }
    }

    // -------------------------------------------------------------------------
    // Feedback banner
    // -------------------------------------------------------------------------

    private void ShowFeedback(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);

        if (_feedbackCoroutine != null)
            StopCoroutine(_feedbackCoroutine);
        _feedbackCoroutine = StartCoroutine(HideFeedbackAfterDelay());
    }

    private void HideFeedback()
    {
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
    }

    private System.Collections.IEnumerator HideFeedbackAfterDelay()
    {
        yield return new WaitForSecondsRealtime(feedbackDuration);
        HideFeedback();
    }

    // -------------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------------

    private ShopSlotUI FindSlotForData(ShopItemData data)
    {
        // Match by ScriptableObject reference
        int idx = shopInventory.IndexOf(data);
        if (idx >= 0 && idx < _slots.Count)
            return _slots[idx];
        return null;
    }
}
