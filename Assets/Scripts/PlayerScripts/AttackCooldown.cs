using UnityEngine;
using UnityEngine.UI;

public class AttackCooldown : MonoBehaviour
{
    public Image cooldownBox;
    public Image backgroundBox;
    public Image grayBox;
    public GameObject player;
    private PlayerController playerController;
    private Vector3 offset = new Vector3(50f, 50f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cooldownOffset = new Vector3(0, (playerController.getAttackCooldownPercentage() - 1) * 25, 0);
        cooldownBox.transform.localScale = new Vector3(1f, playerController.getAttackCooldownPercentage() * 1f, 1f);
        cooldownBox.transform.position = cooldownOffset + offset;
        backgroundBox.transform.position = offset;
        grayBox.transform.position = offset;
    }
}
