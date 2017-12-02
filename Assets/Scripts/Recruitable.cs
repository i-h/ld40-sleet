using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recruitable : Character, Interactable {
    public bool Available = true;
    public Dialog RecruitDialog;
    public void OnTouch(Touch t, Character actor)
    {
        GetRecruited(Player.Instance);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") GetRecruited(Player.Instance);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player") GetRecruited(Player.Instance);
    }
    public void GetRecruited(Player plr)
    {
        if (!Available) return;
        plr.Recruit(this);        
    }

    public struct Stats
    {

    }
}
