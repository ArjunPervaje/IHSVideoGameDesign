using UnityEngine;

public class MovementButtonManager : MonoBehaviour
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
        Debug.Log("Jump Force Increased!");
        playerController.changeJumpForce(0.5f);
    }
}
