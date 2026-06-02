using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    public float speed = 8.0f;
    public float gravityValue = -9.81f;
    private Vector3 gravityDirection;
    public float gravityMultiplier = 0.5f;

    private bool isTrackingPlayer;
    public float trackingRange;
    public float stopTrackingRange;

    private float baseDamage = 10.0f;
    public float damage;
    private HealthControls health;
    public float enemyMultiplier = 1f;
    public GameObject parent;
    private GameManagerScript gameManager;
    private int stage;

    public bool isRangedType;
    private bool attackReady;

    public GameObject projectile;
    public GameObject door;
    private int projectileCount = 0;
    private float projectileAngle = 75.0f;
    public float rangedAttackCooldown = 5f;
    public float projectileBaseDamage = 10;
    private float projectileDamage;
    public float inaccuracyRange;

    public bool isBoss;

    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        this.gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
        this.rb = GetComponent<Rigidbody>();
        this.health = GetComponent<HealthControls>();
        this.damage = this.baseDamage;
        this.attackReady = true;
        this.projectileDamage = this.projectileBaseDamage;
    }

    void Update()
    {
        this.gravityDirection = new Vector3(0, this.gravityValue, 0);
        this.rb.AddForce(gravityDirection * gravityMultiplier, ForceMode.Acceleration);

        this.stage = gameManager.getStage();

        this.damage = this.baseDamage * (1 + (stage / 10));
        this.projectileDamage = this.projectileBaseDamage * (1 + (stage / 10));

        if (this.isRangedType)
        {
            bool withinRange = Mathf.Sqrt(Mathf.Abs(transform.position.x - player.transform.position.x) * Mathf.Abs(transform.position.x - player.transform.position.x) + Mathf.Abs(transform.position.y - player.transform.position.y) * Mathf.Abs(transform.position.y - player.transform.position.y)) <= this.stopTrackingRange;
            this.isTrackingPlayer = Mathf.Sqrt(Mathf.Abs(transform.position.x - player.transform.position.x) * Mathf.Abs(transform.position.x - player.transform.position.x) + Mathf.Abs(transform.position.y - player.transform.position.y) * Mathf.Abs(transform.position.y - player.transform.position.y)) < this.trackingRange
                                 && this.stopTrackingRange < Mathf.Sqrt(Mathf.Abs(transform.position.x - player.transform.position.x) * Mathf.Abs(transform.position.x - player.transform.position.x) + Mathf.Abs(transform.position.y - player.transform.position.y) * Mathf.Abs(transform.position.y - player.transform.position.y));
            projectileCount = (stage / 3) + 1;
            if (withinRange && attackReady)
            {
                StartCoroutine(DoRangedAttack());
            }
        }
        else
        {
            projectileCount = 0;
            this.isTrackingPlayer = Mathf.Abs(transform.position.x - player.transform.position.x) < this.trackingRange;
        }

        if (transform.position.x < player.transform.position.x && this.isTrackingPlayer)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (transform.position.x > player.transform.position.x && this.isTrackingPlayer)
        {
            transform.Translate(Vector3.right * -speed * Time.deltaTime);
        }

        if (isBoss && this.health.getIsHealthDepleted())
        {
            Destroy(parent);
            door.SetActive(true);
        }
        else if (this.health.getIsHealthDepleted())
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

    IEnumerator DoRangedAttack()
    {
        attackReady = false;
        for (int i = 0; i < projectileCount; i++)
        {
            float targetX = player.transform.position.x + Random.Range(-this.inaccuracyRange, this.inaccuracyRange);
            float targetY = player.transform.position.y;

            float initialX = transform.position.x;
            float initialY = transform.position.y;

            float gravity = -9.81f;

            //float velocity = Mathf.Sqrt(
            //                (-gravity * targetX * targetX) / 
            //                (Mathf.Cos(DegreesToRadians(this.projectileAngle)) * Mathf.Cos(DegreesToRadians(this.projectileAngle)) * (initialY - targetY - (Mathf.Tan(DegreesToRadians(this.projectileAngle)) * targetX)))
            //                );
            float velocity = Mathf.Sqrt(
                             (-gravity * (targetX - initialX)) / 
                             (Mathf.Sin(2 * DegreesToRadians(this.projectileAngle)))
                             ) * 1.9f;

            GameObject spawnedProjectile = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

            //Debug.Log("vel: " + velocity);

            //Debug.Log("x vel: " + Mathf.Cos(DegreesToRadians(this.projectileAngle)) * velocity);
            //Debug.Log("y vel: " + Mathf.Sin(DegreesToRadians(this.projectileAngle)) * velocity);

            spawnedProjectile.GetComponent<Rigidbody>().linearVelocity = new Vector3(Mathf.Cos(DegreesToRadians(this.projectileAngle)) * velocity, Mathf.Sin(DegreesToRadians(this.projectileAngle)) * velocity, 0.0f);

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(rangedAttackCooldown);
        attackReady = true;
    }

    public float GetProjectileDamage()
    {
        return this.projectileDamage;
    }

    private float DegreesToRadians(float degrees)
    {
        //Debug.Log(degrees * 3.14f / 180.0f);
        return degrees * 3.14f / 180.0f;
    }
}
