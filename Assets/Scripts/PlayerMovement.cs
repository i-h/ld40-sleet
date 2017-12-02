
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IFollowable {
    enum MovePhase { Idle, Moving, Stalled }
    [SerializeField]
    private float _moveSpeed = 2f;
    private Vector2 _targetPosition;
    private Vector2 _distance;
    private Rigidbody2D _rb;
    private Vector2 _lastPosition;
    private float _stallThreshold = 0.1f;
    [SerializeField]
    private MovePhase _moving = MovePhase.Idle;
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

        switch (_moving)
        {
            case MovePhase.Moving:
                float movedDistance = (posNow - _lastPosition).magnitude;

                if (posNow != _targetPosition)  // If we haven't reached target pos
                {
                    if ((movedDistance < _stallThreshold))  // Check if we're stalled
                    {
                        //_moving = MovePhase.Stalled;
                    }

                    _distance = posNow - _targetPosition;
                    _rb.MovePosition(Vector2.MoveTowards(posNow, _targetPosition, _moveSpeed / 20));
                } else
                {
                    _moving = MovePhase.Idle;
                }

                break;
            case MovePhase.Stalled:
                Debug.Log("Stalled!");
                _targetPosition = posNow;
                _moving = MovePhase.Idle;
                break;
            case MovePhase.Idle:

                break;
        }

        _lastPosition = posNow;
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
            RaycastHit2D rch = Physics2D.Raycast(wPos, Vector2.zero);
            if (rch.collider == null)
            {
                SetTarget(wPos);
            } else
            {
                Interactable ia = rch.collider.GetComponent<Interactable>();
                if (ia != null) ia.OnTouch(t);
            }
        }

        // Other touches
        if (touches.Length > 1)
        {
            for (int i = 0; i < touches.Length; i++)
            {

            }
        }
    }

    void SetTarget(Vector2 pos)
    {
        if (pos != (Vector2)transform.position)
        {
            _targetPosition = pos;
            _moving = MovePhase.Moving;
        }
    }

    public bool GetFocusOn()
    {
        return false;
    }

    public Vector2 GetMoveTarget()
    {
        if (_targetPosition == null || _targetPosition == Vector2.zero)
        {
            return transform.position;
        }
        else
        {
            return _targetPosition;
        }
    }
    public Vector2 GetCurrentPosition()
    {
        return transform.position;
    }
}
