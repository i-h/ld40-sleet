using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recruitable : Character, Interactable {
    public bool Available = true;
    public Stats Attributes = new Stats();
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

    [Serializable]
    public struct Stats
    {
        public byte Programming;
        public byte GraphicDesign;
        public byte GameDesign;
        public byte SoundDesign;
        public byte Social;
        public byte Leadership;
    }
}
