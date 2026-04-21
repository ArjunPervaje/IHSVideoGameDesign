using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject enemy;
    public Camera camera;
    public GameObject player;
    private bool DebugPanelOpen;
    public GameObject DebugPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DebugPanelOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
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

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    Debug.Log("Teleported Player At Cursor");
        //    Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        //    mousePos.z = 0f;
        //    player.transform.position = mousePos;
        //}
    }
}
