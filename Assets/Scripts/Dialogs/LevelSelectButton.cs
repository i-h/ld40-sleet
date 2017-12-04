using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : SceneSelectButton {
    public bool Locked = true;
    public Color LockColor = Color.gray;
    Image _sr;
	// Use this for initialization
	void Awake () {
        _sr = GetComponent<Image>();
	}

    private void Start()
    {
        PlayerPrefs.SetInt("level1", 1);
        SetLocked(PlayerPrefs.GetInt(Target, 0) == 0);
    }

    public void SetLocked(bool locked)
    {
        Locked = locked;
        if (Locked)
        {
            _sr.color = LockColor;
        }
    }

    public override void Change()
    {
        if(!Locked) base.Change();
    }
}
