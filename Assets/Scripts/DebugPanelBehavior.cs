using UnityEngine;
using TMPro;

public class DebugPanelBehavior : MonoBehaviour
{
    public TextMeshProUGUI DebugPanel;
    public GameObject player;
    private PlayerController playerInfo;
    private HealthControls playerHealthInfo;
    private GameManagerScript gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
        playerInfo = player.GetComponent<PlayerController>();
        playerHealthInfo = player.GetComponent<HealthControls>();
    }

    // Update is called once per frame
    void Update()
    {
        DebugPanel.text = "Player Melee Attack CD%: " + playerInfo.getAttackCooldownPercentage() +
                        "\nPlayer Health%: " + playerHealthInfo.getHealthPercentage() * 100 +
                        "\nPlayer Health: " + playerHealthInfo.getCurrentHealth() +
                        "\nStage: " + gameManager.GetStage() + 
                        "\nIs Player Touching Ground: " + playerInfo.getPlayerTouchingGround() +
                        "\nPlayer Current Jumps: " + playerInfo.getCurrentJumpsAvailable();
    }
}
