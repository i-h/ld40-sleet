using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float _moveSpeed = 2f;
    private Vector2 _targetPosition;
    private Vector2 _distance;
    private Rigidbody2D _rb;
	// Use this for initialization
	void Awake () {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
        MoveTowardsTarget();
	}
    
    // Move towards the target position
    void MoveTowardsTarget()
    {
        Vector2 posNow = transform.position;
        if (posNow != _targetPosition)
        {
            _distance = posNow - _targetPosition;
            _rb.MovePosition(Vector2.MoveTowards(posNow, _targetPosition, _moveSpeed * (Random.value / 2 + 0.5f) / 20));
            

        }
        else
        {
        }
    }

    void ReadInput(Touch[] touches)
    {
        // Main touch
        Touch t = touches[0];
        _targetPosition = Camera.main.ScreenToWorldPoint(t.position);

        // Other touches
        if (touches.Length > 1)
        {
            for (int i = 0; i < touches.Length; i++)
            {

            }
        }
    }
}
