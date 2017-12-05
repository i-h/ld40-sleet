using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour {
    private MissionManager _mm;
    public List<TeamSlot> TeamSlots;
    public TeamInfoPanel InfoPanelPrefab;
    public TeamInfoPanel InfoPanel;
    public GameProduct GameProject;
	// Use this for initialization
	void Start () {
        TeamSlots.AddRange(GetComponentsInChildren<TeamSlot>());

        // Instantiate the info panel
        GameObject co = GameObject.FindWithTag("WorldCanvas");
        if(co != null && InfoPanelPrefab != null)
        {
            //Canvas c = co.GetComponent<Canvas>();
            InfoPanel = Instantiate(InfoPanelPrefab, co.transform);
            InfoPanel.AttachedTeam = this;
            InfoPanel.Display();
        }
        _mm = MissionManager.Instance;
        if(_mm == null)
        {
            Debug.LogWarning("No mission manager available!");
            _mm = new GameObject().AddComponent<MissionManager>();
        }
        _mm.ReportAvailableTeam(this);
	}

    public void FinishProject()
    {
        //Debug.Log("Team will disband now");
        _mm.ProjectFinished(this, GameProject);
        GameProject = null;
        InfoPanel.FinishDevelopment();
        ClearTeam();
    }

    void ClearTeam()
    {
        foreach(TeamSlot ts in TeamSlots)
        {
            ts.Person.Dispose();
            ts.Person = null;
        }
        if(InfoPanel != null) InfoPanel.UpdateDisplay();
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
                if (InfoPanel != null) InfoPanel.UpdateDisplay();
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
