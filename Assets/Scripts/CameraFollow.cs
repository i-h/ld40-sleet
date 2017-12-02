using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform Target;
    public bool FocusOnTarget = false;
    Vector2 _focusPoint = Vector2.zero;
    Vector2 _camTarget;
    Vector2 _diff;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 tgtPos = Target.position;
        Vector2 posNow = transform.position;

        if (!FocusOnTarget)
        {
            _camTarget.x = (_focusPoint.x + tgtPos.x) / 2;
            _camTarget.y = (_focusPoint.y + tgtPos.y) / 2;
        } else
        {
            _focusPoint = tgtPos;
            _camTarget = tgtPos;
        }

        _diff = posNow - _camTarget;

        transform.position = Vector3.MoveTowards(posNow, _camTarget, (1+_diff.magnitude)/10) + -Vector3.forward;


	}
}
