using UnityEngine;

public class PlayerParentScript : MonoBehaviour
{
    public static PlayerParentScript instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // delete the NEW duplicate
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
