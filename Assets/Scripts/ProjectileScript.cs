using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 gravityDirection;
    private float gravityValue = -9.81f;
    private GameObject owner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravityDirection = new Vector3(0, gravityValue, 0);
        owner = GameObject.FindWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(gravityDirection, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthControls playerHealth = other.GetComponent<HealthControls>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(owner.GetComponent<EnemyBehavior>().GetProjectileDamage());
            }
        }
    }

    public void SetVelocity1(float x, float y)
    {
        rb.linearVelocity = new Vector3(0.5f, 0.5f, 0.0f);
    }

    public void TestPrint(float x, float y)
    {
        Debug.Log("Test print: " + x + " " + y);
        rb.linearVelocity = new Vector3(x, y, 0.0f);
    }
}
