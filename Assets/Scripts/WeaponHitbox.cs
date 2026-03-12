using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public int damage = 10;
    public GameObject player;
    private PlayerController playerController;
    private Vector3 hitboxRelative = new Vector3(0.6f, 0 ,0);
    private bool 

    private bool didChangeDirection = false;

    void start ()
    {
        gameObject.SetActive(false);
        playerController = player.getComponent<PlayerController>();
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
        bool prevDirection = 
        transform.position = hitboxRelative;
        if ()
        {

        }
    }
}
