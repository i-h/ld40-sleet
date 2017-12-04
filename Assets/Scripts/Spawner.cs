using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public Recruitable[] Recruitables;
    public bool Active = true;
    public float Interval = 5.0f;

	// Use this for initialization
	void Start () {
        Invoke("Spawn", Interval);
	}
	
    void Spawn()
    {
        Vector2 pos = SpawnArea.GetRandomArea().GetRandomPoint();

        Recruitable person = Instantiate(Recruitables[Random.Range(0, Recruitables.Length)]);
        person.Attributes.Social = Random.Range(0, 256);
        person.Attributes.Leadership = Random.Range(0, 256);
        person.transform.position = pos;

        Invoke("Spawn", Interval);
    }
}
