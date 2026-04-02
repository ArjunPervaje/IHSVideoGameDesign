using UnityEngine;

public class HealthBarControls : MonoBehaviour
{
    public GameObject redBar;
    public GameObject greenBar;
    public GameObject attatchedObject;
    private HealthControls healthController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthController = attatchedObject.getComponent<HealthControls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
