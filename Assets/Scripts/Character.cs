using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    SpriteRenderer _r;
    Transform _t;
	// Use this for initialization
	void Awake() {
        _r = GetComponent<SpriteRenderer>();
        _t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        _r.sortingOrder = (int)-_t.position.y;
	}    
}
