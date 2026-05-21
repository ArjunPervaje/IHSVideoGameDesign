using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public GameObject footHitbox;

    public float attackCooldown = 0.25f;
    public GameObject sideWeaponHitbox;
    public GameObject upWeaponHitbox;
    public GameObject downWeaponHitbox;
    private bool canAttack = true;

    public float multiplier = 1.0f;

    private bool isFacingRight;

    private float timeSinceLastAttack;
    private float attackCooldownPercentage;

    void Start()
    {
        this.playerRb = GetComponent<Rigidbody>();
        this.currentJumpsAvailable = this.maxJumps;
        this.playerRb.useGravity = false;
        this.healthController = GetComponent<HealthControls>();
   
    }

    void Update()
    {
        this.isDead = healthController.getIsHealthDepleted();
        if (this.isDead)
        {
            this.gameObject.SetActive(false);
        }
        if (!this.isDead)
        {
            this.gravityDirection = new Vector3(0, this.gravityValue, 0);
            this.horizontalInput = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(Mathf.Abs(horizontalInput), 0, 0);
            transform.Translate(movement * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Space) && this.currentJumpsAvailable > 0)
            {
                this.playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0.0f, playerRb.linearVelocity.z);
                currentJumpsAvailable--;
                this.playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            
            this.playerRb.AddForce(gravityDirection * gravityMultiplier, ForceMode.Acceleration);
            
            if (this.horizontalInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (this.horizontalInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            if (footHitbox.GetComponent<FootHitboxScript>().isTouchingGround())
            {
                this.currentJumpsAvailable = this.maxJumps;
            }
        }

        //attack
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(DoUpAttack());
            }
            else if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(DoDownAttack());
            }
            else
            {
                StartCoroutine(DoSideAttack());
            }

            timeSinceLastAttack = 0.0f;
        }
        timeSinceLastAttack += Time.deltaTime;
        attackCooldownPercentage = timeSinceLastAttack / attackCooldown;
        attackCooldownPercentage = Mathf.Clamp(attackCooldownPercentage, 0f, 1f);
        //Debug.Log(attackCooldownPercentage);
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        currentJumpsAvailable = maxJumps;
    //    }
    //}

    IEnumerator DoSideAttack()
    {
        canAttack = false;
        sideWeaponHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        sideWeaponHitbox.gameObject.SetActive(false);
    }

    IEnumerator DoUpAttack()
    {
        canAttack = false;
        upWeaponHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        upWeaponHitbox.gameObject.SetActive(false);
    }

    IEnumerator DoDownAttack()
    {
        canAttack = false;
        downWeaponHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        downWeaponHitbox.gameObject.SetActive(false);
    }

    public void changeAttack(float amount)
    {
        Debug.Log("Attacked Increased!");
        multiplier *= amount;
    }

    public void changeJump(int amount)
    {
        Debug.Log("Jump Instances Increased!");
        maxJumps += amount;
    }

    public void changeJumpForce(float amount)
    {
        Debug.Log("Jump Force Inreased!");
        jumpForce += amount;
    }

    public float getDamageMultiplier()
    {
        return multiplier;
    }

    public float getAttackCooldownPercentage()
    {
        return attackCooldownPercentage;
    }

    public int getCurrentJumpsAvailable()
    {
        return currentJumpsAvailable;
    }

    public bool getPlayerTouchingGround()
    {
        return footHitbox.GetComponent<FootHitboxScript>().isTouchingGround();
    }

}
