using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManagerScript : MonoBehaviour
{
    public int stage = 1;
    public GameObject gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.FindWithTag("GameManager");
        gameManager.GetComponent<GameManagerScript>().SetStage(stage);
    }

    public void LeaveShop()
    {
        Debug.Log("left shop");
        SceneManager.LoadScene("Sandbox2");
        stage++;
    }
}
