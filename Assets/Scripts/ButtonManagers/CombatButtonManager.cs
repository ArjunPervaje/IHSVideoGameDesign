using UnityEngine;

public class CombatButtonManager : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject button;
    public GameObject player;  

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();  
    }

    void Update() { }

    public void OnButtonClick()
    {
        Debug.Log("Attack Increased!");
        playerController.changeAttack(1.5f);
    }
}
