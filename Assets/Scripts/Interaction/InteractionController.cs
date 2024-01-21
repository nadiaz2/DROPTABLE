using UnityEngine;
using System.Linq;

public class InteractionController : MonoBehaviour
{
    // Mask for the 8th layer (index based)
    // 8th layer = Interaction layer
    public LayerMask layerMask = 1 << 8;
    public int interactionRadius = 5;

    // Update is called once per frame
    void Update()
    {
        var colliders = Physics.OverlapSphere(transform.position, interactionRadius, layerMask.value);
        var ordered = colliders.OrderBy(c => (transform.position - c.transform.position).sqrMagnitude).ToArray();
        if(ordered.Length < 1)
        {
            return;
        }

        Interactable closestInteractable = ordered[0].gameObject.GetComponent<Interactable>();
        closestInteractable.Interact();
    }
}
