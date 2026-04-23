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
    private float baseDamage = 10.0f;
    public float damage;
    private HealthControls health;
    public float enemyMultiplier = 1f;
    public GameObject parent;
    private GameManagerScript gameManager;
    private int stage;

    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        this.gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
        this.rb = GetComponent<Rigidbody>();
        this.health = GetComponent<HealthControls>();
        this.damage = this.baseDamage;
    }

    void Update()
    {
        this.gravityDirection = new Vector3(0, this.gravityValue, 0);
        this.rb.AddForce(gravityDirection * gravityMultiplier, ForceMode.Acceleration);
        this.isTrackingPlayer = Mathf.Abs(transform.position.x - player.transform.position.x) < this.trackingRange;

        this.stage = gameManager.getStage();

        this.damage = this.baseDamage * (1 + (stage / 10));

        if (transform.position.x < player.transform.position.x && this.isTrackingPlayer)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (transform.position.x > player.transform.position.x && this.isTrackingPlayer)
        {
            transform.Translate(Vector3.right * -speed * Time.deltaTime);
        }

        if (this.health.getIsHealthDepleted())
        {
            Destroy(parent);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthControls playerHealth = other.GetComponent<HealthControls>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage * enemyGetDamageMultiplier());
            }
        }

    }

    public float enemyGetDamageMultiplier()
    {
        return enemyMultiplier;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(), true);
        }
    }
}
