using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Character))]
public class WanderAI : MonoBehaviour {
    public float WanderFreq = 3;
    public float WanderRand = 2;
    public float WanderRange = 0.2f;
    Recruitable _controllable;
    Vector2 _originalPosition;
	// Use this for initialization
	void Awake () {
        _controllable = GetComponent<Recruitable>();
        if (_controllable == null) enabled = false;
        _controllable.MoveSpeed = 0.5f;
	}

    private void Start()
    {
        _originalPosition = transform.position;
        Invoke("Wander", WanderFreq + Random.value * WanderRand);
    }

    private void Wander()
    {
        _controllable.SetTarget((Vector2)_originalPosition + Random.insideUnitCircle * WanderRange);
        if(_controllable.Available) Invoke("Wander", WanderFreq + Random.value * WanderRand);
    }

    private void OnDrawGizmos()
    {
        if (Application.isEditor)
        {
            Gizmos.DrawWireSphere(transform.position, WanderRange);
        } else
        {
            Gizmos.DrawWireSphere(_originalPosition, WanderRange);
        }
    }
}
