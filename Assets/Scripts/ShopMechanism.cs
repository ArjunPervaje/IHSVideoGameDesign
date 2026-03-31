// Attach to Door GameObject
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopMechanism : MonoBehaviour
{
    public ShopUpgrades[] upgrades;

    [Header("Player Settings")]
    public string playerTag = "Player";
    public int playerGold = 100;

    // Private UI references (auto-created at runtime)
    private GameObject shopPanel;
    private TMP_Text goldText;
    private Transform itemContainer;
    private bool shopOpen = false;
    private Canvas shopCanvas;

    private void Start()
    {
        shopPanel.SetActive(false);
    }

    private void Update()
    {
        if (shopOpen && Input.GetKeyDown(KeyCode.Escape))
            CloseShop();
    }

    // Trigger Detection

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            OpenShop();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            CloseShop();
    }

    // Shop Logic

    private void OpenShop()
    {
        shopOpen = true;
        shopPanel.SetActive(true);
        RefreshGoldDisplay();
        Time.timeScale = 0f;
    }

    private void CloseShop()
    {
        shopOpen = false;
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void TryPurchase(ShopUpgrades item, GameObject buttonObj)
    {

        if (playerGold >= item.cost)
        {
            playerGold -= item.cost;
            RefreshGoldDisplay();
            RefreshButtons();
            Debug.Log($"Purchased: {item.itemName}");
            // Hook into your own inventory here:
            // playerInventory.AddItem(item.itemName);
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    private void RefreshGoldDisplay()
    {
        if (goldText != null)
            goldText.text = $"Gold: {playerGold}";
    }

    private void RefreshButtons()
    {
        // Implement button state refresh here
    }

}