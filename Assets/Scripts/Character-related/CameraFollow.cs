using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float MaxDistance = 10.0f;
    public float CameraSmooth = 30.0f;
    public Character Target;
    public bool FocusOnTarget = false;
    public Vector2 FocusPoint = Vector2.zero;
    Vector2 _camTarget;
    Vector2 _diff;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector2 tgtPos = Target.GetCurrentPosition();
        Vector2 posNow = transform.position;

        FocusPoint = Target.GetMoveTarget();

        if (!Target.GetFocusOn() && !FocusOnTarget)
        {
            _camTarget.x = (FocusPoint.x*2 + tgtPos.x) / 3;
            _camTarget.y = (FocusPoint.y*2 + tgtPos.y) / 3;
        } else
        {
            FocusPoint = tgtPos;
            _camTarget = tgtPos;
        }
        


        _diff = posNow - _camTarget;

        if (_diff.magnitude > MaxDistance)
        {
            _camTarget = (Vector2)tgtPos + _diff.normalized * MaxDistance * 0.9f;
        }

        float camSpeed = (1.0f + _diff.magnitude) / CameraSmooth;

        transform.position = Vector3.MoveTowards(posNow, _camTarget, camSpeed) - Vector3.forward;


	}
}
