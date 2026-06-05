using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject enemy;
    public Camera camera;
    public GameObject player;
    private bool DebugPanelOpen;
    public GameObject DebugPanel;
    private int stage;
    public Vector3 spawnPoint;
    public int flesh;
    public int upgradeCost;

    public int currency = 0;
    public TextMeshProUGUI currencyText;
    public GameManagerScript Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.transform.position = spawnPoint;

        this.DebugPanelOpen = false;
        this.stage = 1;

        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restarted Scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Spawned Enemy At Cursor");
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            Instantiate(enemy, mousePos, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (!DebugPanelOpen)
            {
                DebugPanel.SetActive(true);
                DebugPanelOpen = true;
            }
            else if (DebugPanelOpen)
            {
                DebugPanel.SetActive(false);
                DebugPanelOpen = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            stage++;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            stage--;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            player.GetComponent<HealthControls>().setPlayerHealthPercentage(1);
        }

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    Debug.Log("Teleported Player At Cursor");
        //    Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        //    mousePos.z = 0f;
        //    player.transform.position = mousePos;
        //}

    }

    public void AttackUpgrade(float amount)
    {
        player.GetComponent<PlayerController>().changeAttack(amount);
    }

    public void JumpForceUpgrade(float amount)
    {
        player.GetComponent<PlayerController>().changeJumpForce(amount);
    }

    public void HealUpgrade(float amount)
    {
        player.GetComponent<HealthControls>().Heal(amount);
    }

    public int GetStage()
    {
        return stage;
    }

    public void LeaveShop()
    {
        Debug.Log("left shop");
        SceneManager.LoadScene("Sandbox2");
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
        currencyText.text = "$" + currency;
    }

    public int GetCurrency()
    {
        return currency;
    }
}
