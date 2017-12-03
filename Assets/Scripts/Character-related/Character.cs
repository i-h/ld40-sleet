using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IFollowable
{
    public bool IsNPC = true;
    public int SortOrderOffset = 0;
    public enum MovePhase { Idle, Moving, Stalled }
    public float MoveSpeed = 2f;
    private Vector2 _targetPosition;
    private Vector2 _distance;
    private Rigidbody2D _rb;
    private Vector2 _lastPosition;
    private float _stallThreshold = 0.1f;
    [SerializeField]
    private MovePhase _moving = MovePhase.Idle;
    private bool _movementDisabled;
    
	// Use this for initialization
	void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        SetNPC(IsNPC);
    }
	
	// Update is called once per frame
	void Update () {
        if (!_movementDisabled)
        {
            MoveTowardsTarget();
        }
        _rb.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
    }
    public Rigidbody2D GetRigidbody()
    {
        return _rb;
    }

    /* Movement */
    public void SetTarget(Vector2 pos)
    {
        if (pos != (Vector2)transform.position)
        {
            _targetPosition = pos;
            _moving = MovePhase.Moving;
        }
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
                    _rb.MovePosition(Vector2.MoveTowards(posNow, _targetPosition, MoveSpeed / 20));
                }
                else
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
    public void DisableMovement()
    {
        _movementDisabled = true;
        _rb.isKinematic = true;
    }
    public void SetNPC(bool isNPC)
    {
        IsNPC = isNPC;
        //if(isNPC) _rb.gravityScale = 0;
    }
    public bool GetFocusOn()
    {
        return false;
    }

    public Vector2 GetMoveTarget()
    {
        if (_targetPosition == Vector2.zero)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Walls")
        {
            SetTarget(transform.position);
        }
    }
}
