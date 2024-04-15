using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    [HideInInspector] public bool goBlacked = true;
    public float fadeSeconds = 2;
    private Image image;
    private float percentFade = 1.0f;

    void Start()
    {
        // set color of the panel transparent
        this.image = GetComponent<Image>();
        this.image.color = goBlacked ? Color.black : Color.clear;
        this.percentFade = goBlacked ? 1.0f : 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

}
