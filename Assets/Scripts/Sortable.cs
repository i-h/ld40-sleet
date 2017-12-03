using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class Sortable : MonoBehaviour {
    public float SortOffset = 0.0f;
    public SpriteRenderer SortableRenderer;
    public Transform SortableTransform;
    public bool AsChild = false;
    float _lastY;

	// Use this for initialization
	void Awake() {
        GetRefs();
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if(SortableTransform == null || SortableRenderer == null)
        {
            Debug.LogWarning("Transform or Renderer was null for " + name);
            GetRefs();
        }
        if (SortableTransform.position.y != _lastY)
        {
            _lastY = SortableTransform.position.y;
        }
        //_r.sortingOrder = LayerOrderer.GetLayerOrder(transform.position.y, (int)SortOffset);
        SortableTransform.position = LayerOrderer.SortTransform(this);

    }
    private void OnTransformParentChanged()
    {
        if (SortableTransform.parent != null)
        {
            AsChild = SortableTransform.parent.GetComponent<Sortable>() != null;
        }
    }
    private void GetRefs()
    {
        SortableRenderer = GetComponent<SpriteRenderer>();
        SortableTransform = GetComponent<Transform>();
        if (SortableTransform.parent != null)
        {
            AsChild = SortableTransform.parent.GetComponent<Sortable>() != null;
        }
        SortableRenderer.sortingLayerName = "Environment";
        SortableRenderer.sortingOrder = 0;
    }
}
