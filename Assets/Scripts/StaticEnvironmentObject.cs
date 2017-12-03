using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StaticEnvironmentObject : MonoBehaviour {
    public int DepthOffset = 0;
    SpriteRenderer _r;
	// Update is called once per frame
	void LateUpdate () {
        UpdateDepth();
    }
    public void UpdateDepth()
    {
        GetR().sortingOrder = LayerOrderer.GetLayerOrder(transform.position.y, DepthOffset);
        //transform.position = LayerOrderer.SortTransform(transform, DepthOffset);
    }
    SpriteRenderer GetR()
    {
        if (_r == null) _r = GetComponent<SpriteRenderer>();
        return _r;
    }
}
