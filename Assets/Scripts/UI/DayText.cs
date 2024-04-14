using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayText : MonoBehaviour
{
    [HideInInspector] public bool goInvisible = false;
    public float fadeSeconds = 2;
    public TextMeshProUGUI dayText;
    private float percentFade = 1.0f;
    private Color invisibleColor = new Color(1f, 1f, 1f, 0f);

    void Start()
    {
        // set color of the panel transparent
        this.dayText.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (!goInvisible && (percentFade != 1.0f))
        {
            percentFade += Time.deltaTime / fadeSeconds;
            percentFade = Mathf.Min(percentFade, 1.0f);
            this.dayText.color = Color.Lerp(invisibleColor, Color.white, percentFade);
        }
        else if(goInvisible && (percentFade != 0.0f))
        {
            percentFade -= Time.deltaTime / fadeSeconds;
            percentFade = Mathf.Max(percentFade, 0.0f);
            this.dayText.color = Color.Lerp(invisibleColor, Color.white, percentFade);
        }
    }

    public void SetText(string text)
    {
        dayText.text = text;
    }

}
