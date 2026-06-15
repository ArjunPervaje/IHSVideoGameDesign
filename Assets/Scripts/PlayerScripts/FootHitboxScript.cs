using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class FootHitboxScript : MonoBehaviour
{
    // Track distinct ground colliders so multiple adjacent cubes don't cause flicker
    private HashSet<Collider> groundContacts = new HashSet<Collider>();
    private float ignoreUntil = 0f; // used to ignore ground briefly after jumping

    void Start()
    {
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundContacts.Add(collision);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundContacts.Add(collision);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundContacts.Remove(collision);
        }
    }

    public bool isTouchingGround()
    {
        if (Time.time < ignoreUntil) return false;
        // Remove any destroyed/null colliders left over after scene changes so Count is accurate
        groundContacts.RemoveWhere(c => c == null);
        return groundContacts.Count > 0;
    }

    // Start a short grace period after jumping so rapid transitions between adjacent ground colliders
    // don't immediately re-ground the player.
    public void StartJumpGrace(float duration)
    {
        ignoreUntil = Time.time + duration;
    }

    // Utility to manually clear contacts (not normally required)
    public void ClearContacts()
    {
        groundContacts.Clear();
    }

}
