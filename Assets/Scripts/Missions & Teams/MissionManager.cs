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
    public int CompletionScore = 0;
    public string NextUnlock = "";
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
            if(GameMode == MissionMode.Endless)
            {
                OnTeamFinishedEndless(team);
            } else
            {

                Instantiate(TeamCompleted, team.transform.position, Quaternion.identity);
            }
        }
        if(CheckUnfinishedTeams() == 0)
        {
            AllTeamsFinished();
        }
    }

    int GetProjectScore(GameProduct p)
    {
        int score = 0;
        score = (int)(2000 * p.Quality / 100.0f + 3000 * 1/(1+p.DevelopmentTime));
        Debug.Log("Project " + p.name + " scored " + score);
        return score;
    }

    internal void ProjectFinished(Team team, GameProduct project)
    {
        Instantiate(TeamCompleted, team.transform.position, Quaternion.identity);
        ScoreManager.Instance.AddScore(GetProjectScore(project));
    }

    private void OnTeamFinishedEndless(Team team)
    {
        GameProduct teamProduct = new GameObject().AddComponent<GameProduct>();
        Transform pt = teamProduct.transform;
        pt.parent = transform;
        pt.name = "Generic Game Name";
        teamProduct.StartDevelopment(team, team.FinishProject);
        team.InfoPanel.StartGameDevelopment(teamProduct);
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
                ScoreManager.Instance.AddScore(CompletionScore);
                CompletionScore = 0;
                if(NextUnlock != "")
                {
                    PlayerPrefs.SetInt(NextUnlock, 1);
                    NextUnlock = "";
                }
                Instantiate(Completed);
            }
        }
    }
}
