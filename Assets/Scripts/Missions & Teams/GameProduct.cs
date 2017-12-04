using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProduct : MonoBehaviour {
    public Team DevTeam;
    Action OnFinishedCallback;
	void Update () {
		
	}
    public void StartDevelopment(Action onFinished)
    {
        OnFinishedCallback = onFinished;

        OnFinishedCallback();
    }
}
