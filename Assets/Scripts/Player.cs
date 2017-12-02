using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
    }

    public void Recruit(Recruitable person)
    {
        person.transform.parent = transform;
        person.transform.localPosition = Vector2.one*0.5f;
    }	
}
