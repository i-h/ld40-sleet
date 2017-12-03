using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleVibrate : MonoBehaviour {
    public float PositionVibrate = 0.2f;
    public float RotationVibrate = 0.2f;
    Vector2 _initialPos;
    Vector3 _initialRotation;
    Vector3 _rotation = new Vector3();

    private void Start()
    {
        _initialPos = transform.position;
        _initialRotation = transform.eulerAngles;
    }
    // Update is called once per frame
    void FixedUpdate () {
        _rotation = _initialRotation;
        _rotation.z += (Random.value * 2 - 1)*RotationVibrate;
        transform.eulerAngles = _rotation;

        transform.position = _initialPos + Random.insideUnitCircle*PositionVibrate;
	}
}
