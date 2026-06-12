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

        this.stage = gameManager.GetStage();

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

            float targetXLocal = targetX;
            float targetYLocal = targetY;

            float initialXLocal = initialX;
            float initialYLocal = initialY;

            float dxSigned = targetXLocal - initialXLocal;
            float dy = targetYLocal - initialYLocal;
            float dx = Mathf.Abs(dxSigned);

            float g = Mathf.Abs(Physics.gravity.y);

            float[] angleCandidates = new float[] { this.projectileAngle, 45f, 35f, 25f, 60f, 15f, 75f };
            float chosenAngleRad = 0f;
            float chosenVelocity = 0f;
            const float EPS = 1e-4f;

            foreach (float angDeg in angleCandidates)
            {
                float aRad = DegreesToRadians(angDeg);
                float cos = Mathf.Cos(aRad);
                float tan = Mathf.Tan(aRad);
                if (Mathf.Abs(cos) < EPS) continue;
                float denom = (dx * tan - dy);
                if (denom <= EPS) continue; 
                float vSq = (g * dx * dx) / (2f * cos * cos * denom);
                if (vSq <= 0f) continue;
                chosenAngleRad = aRad;
                chosenVelocity = Mathf.Sqrt(vSq);
                break;
            }

            GameObject spawned = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody prb = spawned.GetComponent<Rigidbody>();
            if (prb != null)
            {
                prb.useGravity = true;
                prb.linearDamping = 0f;

                if (chosenVelocity > 0f)
                {
                    float vx = Mathf.Cos(chosenAngleRad) * chosenVelocity * Mathf.Sign(dxSigned == 0f ? 1f : dxSigned);
                    float vy = Mathf.Sin(chosenAngleRad) * chosenVelocity;
                    prb.linearVelocity = new Vector3(vx, vy, 0f);
                }
                else
                {
                    Vector3 dir = new Vector3(dxSigned, dy, 0f).normalized;
                    float fallbackSpeed = Mathf.Max(10f, dx * 2f);
                    prb.linearVelocity = dir * fallbackSpeed;
                }
            }

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
        return degrees * Mathf.Deg2Rad;
    }
}
