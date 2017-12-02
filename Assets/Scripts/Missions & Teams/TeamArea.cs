using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamArea : MonoBehaviour {
    public List<TeamSlot> TeamSlots;
	// Use this for initialization
	void Start () {
        TeamSlots.AddRange(GetComponentsInChildren<TeamSlot>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Player")
        {
            c.GetComponent<Player>().PutDownCarried(this);
        }
    }

    public bool AddPerson(Character person)
    {
        foreach(TeamSlot ts in TeamSlots)
        {
            if(ts.Person == null)
            {
                ts.Person = person;
                person.transform.parent = ts.transform;
                person.transform.localPosition = Vector2.zero;
                person.transform.localRotation = Quaternion.identity;
                person.SortOrderOffset = 0;
                return true;
            }
        }

        return false;

    }
}
