using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityFx.Outline;

public class OutlineManager : MonoBehaviour
{
    private OutlineLayer _layer;

    // Start is called before the first frame update
    void Start()
    {
        OutlineEffect outlineEffect = GetComponent<OutlineEffect>();
        outlineEffect.OutlineLayers = ScriptableObject.CreateInstance<OutlineLayerCollection>();
        outlineEffect.OutlineLayers.name = "OutlineLayers";

        _layer = new OutlineLayer("InteractionHighlight");
        //_layer = outlineEffect.OutlineLayers[0];

        _layer.OutlineColor = Color.yellow;
        _layer.OutlineWidth = 10;
        _layer.OutlineRenderMode = OutlineRenderFlags.Blurred;

        outlineEffect.OutlineLayers.Add(_layer);
    }

    public void Highlight(GameObject go)
    {
        if(go != null)
        {
            _layer.Add(go);
        }
    }

    public void Unhighlight(GameObject go)
    {
        if(go != null)
        {
            _layer.Remove(go);
        }
    }
}
