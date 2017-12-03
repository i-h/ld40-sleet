﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {
    public MissionCompleted Completed;
    public static MissionManager Instance;
    Dictionary<TeamArea, bool> TeamList = new Dictionary<TeamArea, bool>();
	// Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReportAvailableTeam(TeamArea team)
    {
        TeamList.Add(team, false);
    }

    public void TeamFinished(TeamArea team)
    {
        TeamList[team] = true;
        if(CheckUnfinishedTeams() == 0)
        {
            AllTeamsFinished();
        }
    }

    int CheckUnfinishedTeams()
    {
        int unfinishedCount = 0;

        foreach (TeamArea t in TeamList.Keys)
        {
            if (!TeamList[t]) unfinishedCount++;
        }

        return unfinishedCount;
    }

    void AllTeamsFinished()
    {
        if(Completed != null)
        {
            Instantiate(Completed);
        }
    }

    public struct Objective
    {
        public TeamArea Team;
        public Recruitable.Stats RequiredTeamStats;
    }
}
