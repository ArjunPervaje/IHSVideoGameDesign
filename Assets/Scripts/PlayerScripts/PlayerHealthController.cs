using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public Image greenHB;
    public Image redHB;
    public GameObject player;
    private HealthControls healthController;
    private Vector3 offset = new Vector3(119.5f, 417f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        healthController = player.GetComponent<HealthControls>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dyingOffset = new Vector3((healthController.getHealthPercentage() - 1) * 100, 0, 0);
        greenHB.transform.localScale = new Vector3(healthController.getHealthPercentage() * 1f, 1f, 1f);
        greenHB.transform.position = dyingOffset + offset;
        redHB.transform.position = offset;
        //if (healthController.getIsHealthDepleted())
        //{
        //    this.gameObject.SetActive(false);
        //}
    }
}
