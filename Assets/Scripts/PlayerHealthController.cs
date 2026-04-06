using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public Image greenHB;
    public Image redHB;
    public GameObject player;
    private HealthControls healthController;
    //private Vector3 offset = new Vector3(-275f, 195f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthController = player.GetComponent<HealthControls>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dyingOffset = new Vector3((healthController.getHealthPercentage() - 1) * 1, 0, 0);
        greenHB.transform.localScale = new Vector3(healthController.getHealthPercentage() * 1f, 1f, 1f);
        //transform.position = offset;
        if (healthController.getIsHealthDepleted())
        {
            this.gameObject.SetActive(false);
        }
    }
}
