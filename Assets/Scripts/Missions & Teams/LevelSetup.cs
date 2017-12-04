using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour {
    public MissionManager.MissionMode Mode = MissionManager.MissionMode.Endless;
    public int CompletionScore = 1000;
    public string UnlocksLevel = "";
    public string InitialHint = "";
    // Use this for initialization
    void Start()
    {
        MissionManager.Instance.GameMode = Mode;
        MissionManager.Instance.CompletionScore = CompletionScore;
        MissionManager.Instance.NextUnlock = UnlocksLevel;
        if(InitialHint != "")
        {
            TutorialManager.Instance.ShowHint(InitialHint);
        }
    }
}
