using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public int damage = 10;
    public GameObject player;
    private PlayerController playerController;
    private Vector3 hitboxRelative = new Vector3(0.6f, 0 ,0);
    

    private bool didChangeDirection = false;

    void start ()
    {
        gameObject.SetActive(false);
        playerController = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthControls enemy = other.GetComponent<HealthControls>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage * playerController.multiplier);
            }
        }
    }

    void update ()
    {
        this.didChangeDirection = false;
        bool prevDirection = playerController.getIsFacingRight(); 
        transform.position = hitboxRelative;
        //if ()
        //{

        //}
    }

}
