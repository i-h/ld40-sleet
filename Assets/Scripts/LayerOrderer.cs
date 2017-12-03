using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOrderer{
    public static int GetLayerOrder(float y, int offset)
    {
        return 0;
        //return (int)-(y * 2) + offset;
    }
    public static Vector3 SortTransform(Sortable t)
    {
        Vector3 pos = t.SortableTransform.position;
        if (!t.AsChild)
        {
            pos.z = pos.y + t.SortOffset;
        } else
        {
            pos.z = t.SortableTransform.parent.position.z - 0.01f;
        }
        return pos;
    }
}
