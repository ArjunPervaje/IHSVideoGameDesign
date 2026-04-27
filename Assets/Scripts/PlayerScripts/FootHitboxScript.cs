using UnityEngine;

public class FootHitboxScript : MonoBehaviour
{
    private bool touchingGround;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }
    }

    public bool isTouchingGround()
    {
        return touchingGround;
    }
}
