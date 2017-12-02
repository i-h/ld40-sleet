
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchController : MonoBehaviour {
    public Character Controllable;
    
	void Start () {
        Controllable.SetNPC(false);
	}
	
	// Update is called once per frame
	void Update () {
        /*
         * Input
         */
        if(Input.touchCount > 0)
        {
            ReadInput(Input.touches);
        }
	}
    

    void ReadInput(Touch[] touches)
    {
        // Main touch
        Touch t = touches[0];
        // Check if we touch inside the viewport
        Vector2 vpPos = Camera.main.ScreenToViewportPoint(t.position);
        Vector2 wPos = Camera.main.ScreenToWorldPoint(t.position);
        if (vpPos.y >= 0 && vpPos.y <= 1)
        {
            Controllable.SetTarget(wPos);

        }

        // Other touches
        if (touches.Length > 1)
        {
            for (int i = 0; i < touches.Length; i++)
            {

            }
        }
    }
}
