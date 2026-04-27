using UnityEngine;
[ExecuteAlways]
public class AutoParent : MonoBehaviour
{
    public string tagName = "PaintedObject";

    void LateUpdate()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Ground");

        foreach (GameObject obj in objs)
        {
            if (obj.transform.parent == null)
            {
                obj.transform.SetParent(transform);
            }
        }
    }
}