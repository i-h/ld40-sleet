using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RectTransform))]
public class Dialog : MonoBehaviour {
    RectTransform _t;
    private void Awake()
    {
        _t = GetComponent<RectTransform>();
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public virtual void Show()
    {

    }
    public virtual void Hide()
    {

    }
}
