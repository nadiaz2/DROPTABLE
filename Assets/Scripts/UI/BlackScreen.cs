using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{

    //public Animator animator;

    //private bool goBlacked = true;
    //private bool goTransparent = false;

    [HideInInspector] public bool goBlacked = true;
    public float fadeSeconds = 2;
    private Image image;
    private float percentFade = 1.0f;

    void Start()
    {
        // set color of the panel transparent
        this.image = GetComponent<Image>();
        this.image.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"{goBlacked} ::: {percentFade} ::: {this.image.color}");
        if (goBlacked && (percentFade != 1.0f))
        {
            percentFade += Time.deltaTime / fadeSeconds;
            percentFade = Mathf.Min(percentFade, 1.0f);
            this.image.color = Color.Lerp(Color.clear, Color.black, percentFade);
        }
        else if(!goBlacked && (percentFade != 0.0f))
        {
            percentFade -= Time.deltaTime / fadeSeconds;
            percentFade = Mathf.Max(percentFade, 0.0f);
            this.image.color = Color.Lerp(Color.clear, Color.black, percentFade);
        }

        /*
        if ((LivingRoomManager.state == LivingRoomState.FinishedTalking) && goBlacked)
        {
            //call GoBlack function after a couple of seconds 
            Invoke("GoBlack", 3);
        }
        if ((LivingRoomManager.state == LivingRoomState.FinishedTalking) && goTransparent && !goBlacked)
        {
            //call GoTransparent function after 2 seconds 
            Invoke("GoTransparent", 3);

            // Sets new game state with delay
            Invoke("classOver", 5);
            
        }
        */
    }

    /*
    void GoBlack()
    {
        // set color of the panel - black
        //GameObject.Find("Canvas/BlackScreen").GetComponent<Image>().color = new Color(0, 0, 0, 255);
        animator.SetBool("SceneVisible", false);
        goBlacked = false;
        goTransparent = true;
    }

    void GoTransparent()
    {
        // set color of the panel transparent
        //GameObject.Find("Canvas/BlackScreen").GetComponent<Image>().color = new Color(0, 0, 0, 0);
        animator.SetBool("SceneVisible", true);
        goTransparent = false;
    }

    
    void classOver()
    {
        ClassroomManager.state = ClassroomState.ClassOver;
    }
    */

}
