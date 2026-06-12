using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 gravityDirection;
    private float gravityValue = -15f;
    private GameObject owner;
    private float timer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Use Physics.gravity so EnemyBehavior's ballistic calculation and projectile gravity match.
        gravityDirection = Physics.gravity;
        owner = GameObject.FindWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        // Only apply manual gravity if the Rigidbody is not already using built-in gravity.
        if (rb != null && !rb.useGravity)
        {
            rb.AddForce(gravityDirection, ForceMode.Acceleration);
        }
        //Debug.Log("x: " + rb.linearVelocity.x);
        //Debug.Log("y: " + rb.linearVelocity.y);

        timer += Time.deltaTime;

        if (timer >= 5.0f)
        {
            Destroy(gameObject);
        }
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

            Destroy(gameObject);
        }
    }

    //public void SetVelocity1(float x, float y)
    //{
    //    rb.linearVelocity = new Vector3(0.5f, 0.5f, 0.0f);
    //}

    //public void TestPrint(float x, float y)
    //{
    //    Debug.Log("Test print: " + x + " " + y);
    //    rb.linearVelocity = new Vector3(x, y, 0.0f);
    //}
}
