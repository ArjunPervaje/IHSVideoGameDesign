
using UnityEngine;

public class ShopUpgrades : ScriptableObject
{
    //public string itemName;
    //public Sprite icon;
    //public int cost;
    //public string description;

    //public Button movementUpgrades;
    //public Button combatUpgrades;
    //public Button passiveUpgrades;

    //private List<string> movementUp = new List<string>
    //{
    //    "Jump Power", "Dash", "Speed Up", "Wall Climb"
    //};

    //private List<string> combatUp = new List<string>
    //{
    //    "Shield", "Parry", "Attack Power"
    //};

    //private List<string> passiveUp = new List<string>
    //{
    //    "Add Health", "Invisibility", "Charm", "Equips"
    //};


    //void Start()
    //{
    //    movementUpgrades.onClck.AddListener(() => OnNewButtonClick("movements"));
    //    combatUpgrades.onClck.AddListener(() => OnNewButtonClick("combats"));
    //    passiveUpgrades.onClck.AddListener(() => OnNewButtonClick("passives"));
    //}

    //void OnButtonClick(string buttonName)
    //{
    //    switch (buttonName)
    //    {

    //        case "movements":
    //            // clear old buttons
    //            foreach (Transform child in buttonContainer)
    //                Destroy(child.gameObject);

    //            // shuffle and pick 3
    //            List<string> shuffled = GetShuffled(movementUp);
    //            List<string> selected = shuffled.GetRange(0, 3);

    //            foreach (string upgrade in selected)
    //            {
    //                Button newButton = Instantiate(buttonPrefab, buttonContainer);
    //                newButton.GetComponentInChildren<Text>().text = upgrade;

    //                string upgradeName = upgrade;
    //                newButton.onClick.AddListener(() => SelectUpgrade(upgradeName));
    //            }
    //            break;

    //        case "combats":
    //            // clear old buttons
    //            foreach (Transform child in buttonContainer)
    //                Destroy(child.gameObject);

    //            // shuffle and pick 3
    //            List<string> shuffled = GetShuffled(combatUp);
    //            List<string> selected = shuffled.GetRange(0, 3);

    //            foreach (string upgrade in selected)
    //            {
    //                Button newButton = Instantiate(buttonPrefab, buttonContainer);
    //                newButton.GetComponentInChildren<Text>().text = upgrade;

    //                string upgradeName = upgrade;
    //                newButton.onClick.AddListener(() => SelectUpgrade(upgradeName));
    //            }
    //            break;

    //        case "passives":
    //            // clear old buttons
    //            foreach (Transform child in buttonContainer)
    //                Destroy(child.gameObject);

    //            // shuffle and pick 3
    //            List<string> shuffled = GetShuffled(passiveUp);
    //            List<string> selected = shuffled.GetRange(0, 3);

    //            foreach (string upgrade in selected)
    //            {
    //                Button newButton = Instantiate(buttonPrefab, buttonContainer);
    //                newButton.GetComponentInChildren<Text>().text = upgrade;

    //                string upgradeName = upgrade;
    //                newButton.onClick.AddListener(() => SelectUpgrade(upgradeName));
    //            }
    //            break;
    //    }
    //}


}
