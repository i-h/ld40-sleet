using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {
    public enum MissionMode { FillAll, Endless }
    public MissionMode GameMode = MissionMode.FillAll;
    public MissionCompleted Completed;
    public MissionCompleted TeamCompleted;
    public static MissionManager Instance;
    Dictionary<Team, bool> TeamList = new Dictionary<Team, bool>();
	// Use this for initialization
	void Awake () {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReportAvailableTeam(Team team)
    {
        TeamList.Add(team, false);
    }

    public void TeamFinished(Team team)
    {
        TeamList[team] = true;
        if (TeamCompleted != null)
        {
            Instantiate(TeamCompleted, team.transform.position, Quaternion.identity);
            if(GameMode == MissionMode.Endless)
            {
                OnTeamFinishedEndless(team);
            }
        }
        if(CheckUnfinishedTeams() == 0)
        {
            AllTeamsFinished();
        }
    }

    private void OnTeamFinishedEndless(Team team)
    {
        GameProduct teamProduct = new GameObject().AddComponent<GameProduct>();
        Transform pt = teamProduct.transform;
        pt.parent = transform;
        pt.name = "Generic Game Name";
        teamProduct.StartDevelopment(team, team.FinishProject);
    }

    int CheckUnfinishedTeams()
    {
        int unfinishedCount = 0;

        foreach (Team t in TeamList.Keys)
        {
            if (!TeamList[t]) unfinishedCount++;
        }

        return unfinishedCount;
    }

    void AllTeamsFinished()
    {
        if (GameMode == MissionMode.FillAll)
        {
            if (Completed != null)
            {
                Instantiate(Completed);
            }
        }
    }
}
