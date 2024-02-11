using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InteractionController : MonoBehaviour
{
    public OutlineManager outlineManager;
    public Text prompt;
    public int interactionRadius = 5;

    private GameObject lastClosest = null;
    private Interactable target = null;
    private GameObject[] targets;

    void Start()
    {
        //targets = GameObject.FindGameObjectsWithTag("Interactable");
        //Debug.Log(targets.Length);
    }

    // Update is called once per frame
    void Update()
    {
        targets = GameObject.FindGameObjectsWithTag("Interactable");

        // ignore class if no interactables
        if (targets.Length < 1)
        {
            return;
        }

        GameObject[] ordered = targets.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).ToArray();

        // Determine closest interactable object
        GameObject closest = null;
        int index = 0;
        while ((index < ordered.Length) && (closest == null))
        {
            Interactable interactable = ordered[index].GetComponent<Interactable>();
            if (interactable.IsActive())
            {
                closest = ordered[index];
            }
            index++;
        }

        // Check if closest is within range
        if (closest != null) // null if no interactables are active
        {
            float distance = (transform.position - closest.transform.position).magnitude;
            if (distance > interactionRadius)
            {
                closest = null;
            }
        }

        // Change interaction target
        if (closest != lastClosest)
        {
            //Debug.Log($"Closest Interactable: {closest}");
            outlineManager.Unhighlight(lastClosest);
            outlineManager.Highlight(closest);

            lastClosest = closest;
            target = closest?.GetComponent<Interactable>();

            string targetPrompt = target?.GetPrompt();
            prompt.text = (targetPrompt == null) ? "" : $"[E] {targetPrompt}";
        }

        // If interact button pressed, interact with object
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            target?.Interact();
        }
    }
}
