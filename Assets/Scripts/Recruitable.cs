using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recruitable : Character, Interactable {
    bool _available = true;
    public Dialog RecruitDialog;
    public void OnTouch(Touch t, Character actor)
    {
        GetRecruited(Player.Instance);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GetRecruited(Player.Instance);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetRecruited(Player.Instance);
    }
    public void GetRecruited(Player plr)
    {
        if (!_available) return;
        plr.Recruit(this);        
    }

    public struct Stats
    {

    }
}
