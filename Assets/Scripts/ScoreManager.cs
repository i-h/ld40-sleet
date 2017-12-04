using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance;
    public Text ScoreDisplay;
    private int _totalScore = 0;
    // Use this for initialization
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            _totalScore = PlayerPrefs.GetInt("Score", 0);
        } else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ScoreDisplay = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        ScoreDisplay.text = "SCORE\n" + _totalScore;
    }
    internal void AddScore(int completionScore)
    {
        _totalScore += completionScore;
        PlayerPrefs.SetInt("Score", _totalScore);
        if (ScoreDisplay != null)
        {
            ScoreDisplay.text = "SCORE\n" + _totalScore;
        }
    }
}
