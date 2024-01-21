using UnityEngine;
using System.Linq;

public class InteractionController : MonoBehaviour
{
    public OutlineManager outlineManager;
    public int interactionRadius = 5;

    private GameObject lastClosest = null;
    private GameObject[] targets;

    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Interactable");
        Debug.Log(targets.Length);
    }

    // Update is called once per frame
    void Update()
    {
        // ignore class if no interactables
        if(targets.Length < 1)
        {
            return;
        }

        GameObject[] ordered = targets.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).ToArray();
        GameObject closest = ordered[0];
        float distance = (transform.position - closest.transform.position).magnitude;
        if(distance > interactionRadius)
        {
            closest = null;
        }
        //Debug.Log($"{distance} {closest}");
        if (closest != lastClosest)
        {
            outlineManager.Unhighlight(lastClosest);
            outlineManager.Highlight(closest);
            lastClosest = closest;
            Debug.Log("new interaction!");
        }

        //lastClosest.GetComponent<Interactable>().Interact();
    }
}
