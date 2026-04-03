using UnityEngine;

public class HealthBarControls : MonoBehaviour
{
    public GameObject redBar;
    public GameObject greenBar;
    public GameObject attatchedObject;
    private HealthControls healthController;
    private Vector3 offset = new Vector3(0f, 2f, -6f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthController = attatchedObject.GetComponent<HealthControls>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dyingOffset = new Vector3((healthController.getHealthPercentage() - 1) * 1, 0, 0);
        greenBar.transform.position = transform.position + dyingOffset;
        Vector3 desiredPosition = attatchedObject.transform.position + offset;
        greenBar.transform.localScale = new Vector3(healthController.getHealthPercentage() * 2f, 0.5f, 1f);
        transform.position = desiredPosition;
        if (healthController.getIsHealthDepleted())
        {
            this.gameObject.SetActive(false);
        }
        if (healthController.getHealthPercentage() == 1f)
        {
            greenBar.GetComponent<MeshRenderer>().enabled = false;
            redBar.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            greenBar.GetComponent<MeshRenderer>().enabled = true;
            redBar.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
