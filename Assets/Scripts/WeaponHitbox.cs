using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public int damage = 10;
    private float knockback = 3f;
    public bool isPlayer;
    //public bool isDownAttack;
    public GameObject owner;
    private Vector3 hitboxRelative = new Vector3(0.6f, 0 ,0);

    void Start ()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthControls enemyHealth = other.GetComponent<HealthControls>();
            Rigidbody enemyRb = other.GetComponent<Rigidbody>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage * this.owner.GetComponent<PlayerController>().getDamageMultiplier());
                Vector3 enemyDirection = (other.transform.position - gameObject.transform.position).normalized;
                if (enemyRb != null)
                {
                    enemyRb.AddForce(enemyDirection * knockback, ForceMode.Impulse);
                }
            }
        }
    }

    void Update ()
    {
        
    }

}
