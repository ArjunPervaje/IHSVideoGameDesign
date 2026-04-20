using UnityEngine;
using TMPro;

public class DebugPanelBehavior : MonoBehaviour
{
    public TextMeshProUGUI DebugPanel;
    public GameObject player;
    private PlayerController playerInfo;
    private HealthController playerHealthInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInfo = player.GetComponent<PlayerController>();
        playerHealthInfo = player.GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        DebugPanel.text = "Player Melee Attack CD%: " + playerInfo.getAttackCooldownPercentage() +
                       "\n Player Health: " + playerHealthInfo.getHealthPercentage() +
                       "\n Player Health%: " + playerHealthInfo.getHealthPercentage();
    }
}
