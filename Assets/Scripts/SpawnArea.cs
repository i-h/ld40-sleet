using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour {
    public static List<SpawnArea> SpawnAreas = new List<SpawnArea>();
    public float Radius = 5;
    private void Awake()
    {
        if(!SpawnAreas.Contains(this)) SpawnAreas.Add(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
    public Vector2 GetRandomPoint()
    {
        return (Vector2)transform.position + Random.insideUnitCircle * Radius;
    }
    public static SpawnArea GetRandomArea()
    {
        return SpawnAreas[Random.Range(0, SpawnAreas.Count)];
    }
}
