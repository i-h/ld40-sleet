using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamArea : MonoBehaviour {
    private MissionManager _mm;
    public List<TeamSlot> TeamSlots;
	// Use this for initialization
	void Start () {
        TeamSlots.AddRange(GetComponentsInChildren<TeamSlot>());
        _mm = MissionManager.Instance;
        if(_mm == null)
        {
            Debug.LogWarning("No mission manager available!");
            _mm = new GameObject().AddComponent<MissionManager>();
        }
        _mm.ReportAvailableTeam(this);
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
                person.transform.localPosition = Vector2.zero + ts.PositionOffset;
                person.transform.localRotation = Quaternion.identity;
                person.SortOrderOffset = 0;

                if (GetFreeSlots() == 0)
                {
                    _mm.TeamFinished(this);
                }
                return true;
                
            }
        }

        return false;

    }

    public int GetFreeSlots()
    {
        int slotCount = 0;
        for(int i = 0; i < TeamSlots.Count; i++)
        {
            if (TeamSlots[i].Person == null) slotCount++;
        }
        return slotCount;
    }
}
