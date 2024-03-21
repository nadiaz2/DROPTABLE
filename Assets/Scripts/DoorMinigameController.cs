using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DoorMinigameController : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject minigameUI;
    public Scrollbar progressBar;
    public Scrollbar alertBar;

    [Header("Speed Parameters")]
    public float progressFillSpeed;
    public float progressEmptySpeed;
    public float alertFillSpeed;
    public float alertEmptySpeed;

    private bool minigameActive;
    private Action<bool> callback;
    private float mouseHeldTime;

    // Start is called before the first frame update
    void Start()
    {
        minigameUI.SetActive(false);

        progressBar.size = 0.0f;
        alertBar.size = 0.0f;
        minigameActive = false;
        callback = null;
        mouseHeldTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!minigameActive)
        {
            return;
        }

        if ((alertBar.size >= 0.75f) || (progressBar.size >= 1.0f))
        {
            EndMinigame(false);
            return;
        }

        // Check for E press
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Win if bar is in the success range
            EndMinigame((progressBar.size > 0.6f) && (progressBar.size < 0.85f));
            return;
        }

        // Check for Left Click
        if (Input.GetMouseButton(0))
        {
            this.mouseHeldTime += Time.deltaTime;

            progressBar.size += 0.001f * progressFillSpeed * Time.deltaTime;
            alertBar.size += (0.001f * alertFillSpeed * this.mouseHeldTime) * (0.001f * alertFillSpeed * this.mouseHeldTime);
        }
        else
        {
            progressBar.size -= 0.001f * progressEmptySpeed * Time.deltaTime;
            alertBar.size -= 0.001f * alertEmptySpeed * Time.deltaTime;
            this.mouseHeldTime = 0.0f;
        }
    }

    public void StartMinigame(Action<bool> callback)
    {
        progressBar.size = 0.0f;
        alertBar.size = 0.0f;
        this.callback = callback;
        mouseHeldTime = 0.0f;

        minigameUI.SetActive(true);
        minigameActive = true;
    }

    private void EndMinigame(bool result)
    {
        this.minigameActive = false;
        this.callback.Invoke(result);
        minigameUI.SetActive(false);
    }
}
