using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance;
     Character _carried;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
    }

    public void Recruit(Recruitable person)
    {
        if (_carried != null) return;
        person.Available = false;
        WanderAI wander = person.GetComponent<WanderAI>();
        if(wander != null)
        {
            wander.enabled = false;
        }
        person.DisableMovement();
        person.CancelInvoke();
        person.transform.parent = transform;
        person.transform.localPosition = Vector2.up;
        person.transform.localEulerAngles = new Vector3(0, 0, 90);
        person.SortOrderOffset = 2;
        foreach(Collider2D c in person.transform.GetComponents<Collider2D>())
        {
            c.enabled = false;
        }
        person.GetRigidbody().Sleep();
        _carried = person;
    }
    public void PutDownCarried(TeamArea dest)
    {
        if (_carried != null)
        {
            if (dest.AddPerson(_carried))
            {
                _carried = null;
            }
        }
    }
}
