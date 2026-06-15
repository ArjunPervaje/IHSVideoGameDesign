using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    private float horizontalInput = 0;
    public int maxJumps = 1;
    public float jumpForce;
    private Rigidbody playerRb;
    private int currentJumpsAvailable;
    // public float gravityValue = -9.81f;
    private Vector3 gravityDirection;
    public float gravityMultiplier;
    private HealthControls healthController;
    private bool isDead;
    public GameObject footHitbox;
    
    private float xVel = 0;
    private float yVel = 0;
    private bool jumpRequested = false;

    public float attackCooldown = 0.25f;
    public GameObject sideWeaponHitbox;
    public GameObject upWeaponHitbox;
    public GameObject downWeaponHitbox;
    private bool canAttack = true;

    public float multiplier;

    private bool isFacingRight;

    private float timeSinceLastAttack;
    private float attackCooldownPercentage;
    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
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
            return;
        }

        // Read inputs
        this.horizontalInput = Input.GetAxis("Horizontal");
        xVel = horizontalInput * speed;

        if (horizontalInput == 0 && footHitbox.GetComponent<FootHitboxScript>().isTouchingGround())
        {
            // play idle
            // anim.SetBool("Idle", true);
            anim.SetBool("isJumping", false);
            anim.SetBool("isMoving", false);
        }
        else if (horizontalInput != 0 && footHitbox.GetComponent<FootHitboxScript>().isTouchingGround())
        {
            // play walk/run
            anim.SetBool("isMoving", true);
            anim.SetBool("isJumping", false);
        }
        else if (!footHitbox.GetComponent<FootHitboxScript>().isTouchingGround())
        {
            // play jump
            anim.SetBool("isJumping", true);
            anim.SetBool("isMoving", false);
        }
        

        if (footHitbox.GetComponent<FootHitboxScript>().isTouchingGround())
        {
            this.currentJumpsAvailable = this.maxJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && this.currentJumpsAvailable > 0)
        {
            currentJumpsAvailable--;
            jumpRequested = true;
        }

        if (this.horizontalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (this.horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //attack
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(DoSideAttack());
            }
            else if (Input.GetKey(KeyCode.S) && !footHitbox.GetComponent<FootHitboxScript>().isTouchingGround())
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
    }

    void FixedUpdate()
    {
        // Apply gravity in fixed timestep
        if (!footHitbox.GetComponent<FootHitboxScript>().isTouchingGround())
        {
            yVel -= gravityMultiplier * Time.fixedDeltaTime;
        }
        else
        {
            if (!jumpRequested)
                yVel = 0f;
        }

        // If a jump was requested this frame, apply it here and start a short grace period
        if (jumpRequested)
        {
            var foot = footHitbox.GetComponent<FootHitboxScript>();
            if (foot != null) foot.StartJumpGrace(0.12f); // tweak duration as needed
            yVel = jumpForce;
            jumpRequested = false;
        }

        // Apply velocity to the Rigidbody
        this.playerRb.linearVelocity = new Vector3(xVel, yVel, 0);
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
        anim.SetBool("isAttacking", true);
        canAttack = false;
        sideWeaponHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        anim.SetBool("isAttacking", false);
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
        anim.SetBool("isPlunging", true);
        canAttack = false;
        downWeaponHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        anim.SetBool("isPlunging", false);
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
