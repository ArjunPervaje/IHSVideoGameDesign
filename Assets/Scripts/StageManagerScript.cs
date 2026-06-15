using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManagerScript : MonoBehaviour
{
    public static StageManagerScript Instance;
    public int stage = 0;
    public int currency = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LeaveShop()
    {
        Debug.Log("left shop");
        stage++;
        SceneManager.LoadScene("OpeningMap");
    }

    public void EnterShop()
    {
        Debug.Log("left shop");
        stage++;
        SceneManager.LoadScene("Shop");
    }

    public void SaveCurrency(int amount)
    {
        currency = amount;
    }

}
