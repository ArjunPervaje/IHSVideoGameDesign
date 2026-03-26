
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Shop/Item")]
public class ShopUpgrades : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int cost;
    public string description;
}
