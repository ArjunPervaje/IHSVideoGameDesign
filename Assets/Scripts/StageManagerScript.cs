using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManagerScript : MonoBehaviour
{
    public static StageManagerScript Instance;
    public int stage = 1;
    public int currency = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void LeaveShop()
    {
        Debug.Log("left shop");
        stage++;
        SceneManager.LoadScene("Sandbox2");
    }

    public void SaveCurrency(int amount)
    {
        currency = amount;
    }

}
