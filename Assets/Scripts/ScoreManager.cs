using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance;
    private int _totalScore = 0;
    // Use this for initialization
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }
    }
    internal void AddScore(int completionScore)
    {
        _totalScore += completionScore;
        PlayerPrefs.SetInt("Score", _totalScore);
    }
}
