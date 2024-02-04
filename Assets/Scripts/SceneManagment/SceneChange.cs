using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string SceneName;
    public GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "OnWayHome")
            {
                OnWayHomeManager.currentInstance.FadeOut();
            }
            Invoke("ChangeScene", 2);
        }
    }

    private void ChangeScene()
    {
        GameManager.state = gameState;
        SceneManager.LoadScene(SceneName);
    }
}
