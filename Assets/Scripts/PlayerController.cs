using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private float horizontalInput = 0;
    public int maxJumps = 1;
    public float jumpForce = 300.0f;
    private Rigidbody playerRb;
    private int currentJumpsAvailable;
    public float gravityValue = -9.81f;
    private Vector3 gravityDirection;
    public float gravityMultiplier = 0.5f;
    private HealthControls healthController;
    private bool isDead;

    public float attackCooldown = 0.5f;
    public GameObject weaponHitbox;
    private bool canAttack = true;

    public float multiplier = 1.0f;

    private bool isFacingRight;
    void Start()
    {
        this.playerRb = GetComponent<Rigidbody>();
        this.currentJumpsAvailable = this.maxJumps;
        this.playerRb.useGravity = false;
        this.healthController = GetComponent<HealthControls>();
        this.isFacingRight = true;
   
    }

    void Update()
    {
        this.isDead = healthController.getIsHealthDepleted();
        if (!this.isDead)
        {
            this.gravityDirection = new Vector3(0, this.gravityValue, 0);
            this.horizontalInput = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(horizontalInput, 0, 0);
            transform.Translate(movement * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Space) && this.currentJumpsAvailable > 0)
            {
                this.playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0.0f, playerRb.linearVelocity.z);
                currentJumpsAvailable--;
                this.playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            
            this.playerRb.AddForce(gravityDirection * gravityMultiplier, ForceMode.Acceleration);
            
            if (horizontalInput != 0)
            {
                if (horizontalInput > 0)
                {
                    this.isFacingRight = true;
                }
                else if (horizontalInput < 0)
                {
                    this.isFacingRight = false;
                }
            }
        }

        //attack
        if (Input.GetMouseButtonDown(0) && canAttack)
        {

            StartCoroutine(DoAttack());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentJumpsAvailable = maxJumps;
        }
    }

    IEnumerator DoAttack()
    {
        canAttack = false;
        weaponHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        weaponHitbox.gameObject.SetActive(false);
    }

    public bool getIsFacingRight()
    {
        return this.isFacingRight;
    }

    public void changeAttack(float amount)
    {
        multiplier *= amount;
    }

    public void changeJump(int amount)
    {
        maxJumps += amount;
    }

    public void changeJumpFore(float amount)
    {
        jumpForce *= amount;
    }

}
