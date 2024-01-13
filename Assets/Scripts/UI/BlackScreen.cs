using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{

    private bool goBlacked = true;
    private bool goTransparent = false;

    void Start()
    {
        // set color of the panel transparent
        GameObject.Find("Canvas/BlackScreen").GetComponent<Image>().color = new Color(0, 0, 0, 0);

    }
  
    // Update is called once per frame
    void Update()
    {
        if ((GameManager.state == GameState.FinishedTalking) && goBlacked)
        {
            //call GoBlack function after a couple of seconds 
            Invoke("GoBlack", 3);
        }
        if ((GameManager.state == GameState.FinishedTalking) && goTransparent && !goBlacked)
        {
            //call GoTransparent function after 2 seconds 
            Invoke("GoTransparent", 2);
        }
    }

    void GoBlack()
    {
        // set color of the panel - black
        GameObject.Find("Canvas/BlackScreen").GetComponent<Image>().color = new Color(0, 0, 0, 255);
        goBlacked = false;
        goTransparent = true;
    }

    void GoTransparent()
    {
        // set color of the panel transparent
        GameObject.Find("Canvas/BlackScreen").GetComponent<Image>().color = new Color(0, 0, 0, 0);
        goTransparent = false;
    }


}
