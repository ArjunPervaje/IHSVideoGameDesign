using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public int damage = 10;
    public bool isPlayer;
    public GameObject owner;
    private Vector3 hitboxRelative = new Vector3(0.6f, 0 ,0);
    private float multiplier = 1;

    void Start ()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthControls enemy = other.GetComponent<HealthControls>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage * this.owner.GetComponent<PlayerController>().getDamageMultiplier());
            }
        }
    }

    void Update ()
    {
        
    }

}
