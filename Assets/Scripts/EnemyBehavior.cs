using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    public float speed = 8.0f;
    public float gravityValue = -9.81f;
    private Vector3 gravityDirection;
    public float gravityMultiplier = 0.5f;
    private bool isTrackingPlayer;
    public float trackingRange = 20.0f;
    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        this.rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        this.gravityDirection = new Vector3(0, this.gravityValue, 0);
        this.rb.AddForce(gravityDirection * gravityMultiplier, ForceMode.Acceleration);
        this.isTrackingPlayer = Mathf.Abs(transform.position.x - player.transform.position.x) < this.trackingRange;

        if (transform.position.x < player.transform.position.x && this.isTrackingPlayer)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (transform.position.x > player.transform.position.x && this.isTrackingPlayer)
        {
            transform.Translate(Vector3.right * -speed * Time.deltaTime);
        }
    }
}
