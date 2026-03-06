using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public int damage = 10;
    public Transform player;
    public Vector3 hitboxRelative;

    void start ()
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
                enemy.TakeDamage(damage);
            }
        }
    }

    void update ()
    {
        Vector3 hitboxPosition = player.position + hitboxRelative;
        transform.position = hitboxPosition;
    }
}
