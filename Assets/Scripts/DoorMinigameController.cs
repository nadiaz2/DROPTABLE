using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorMinigameController : MonoBehaviour
{
    public Scrollbar progressBar;
    public Scrollbar alertBar;

    private bool minigameActive;

    // Start is called before the first frame update
    void Start()
    {
        minigameActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!minigameActive) {
            return;
        }

        // Bar is in the success range
        if((progressBar.size > 0.6) && (progressBar.size < 0.85)) {
            //TODO: check if pressed E
        }
    }
}
