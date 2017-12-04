using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBGM : MonoBehaviour {
    public AudioClip BackgroundMusic;
	// Use this for initialization
	void Start () {
        if(SoundPlayer.Instance == null)
        {
            new GameObject().AddComponent<SoundPlayer>();
        }
		if(BackgroundMusic != null)
        {
            SoundPlayer.Instance.PlayBGM(BackgroundMusic);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
