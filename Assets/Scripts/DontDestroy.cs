using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private string objectID;

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        objectID = scene.name + name + transform.position.ToString();

        var existingObjects = Object.FindObjectsOfType<DontDestroy>();
        for (int i = 0; i < existingObjects.Length; i++)
        {
            if ((existingObjects[i] != this) && (existingObjects[i].objectID == objectID))
            {
                Destroy(gameObject);
            }

        }
        DontDestroyOnLoad(gameObject); // Runs only if object is not destroyed
    }
}
