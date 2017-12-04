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

}
[Serializable]
public struct Stats
{
    [Range(0, 255)]
    public int Programming;
    [Range(0, 255)]
    public int GraphicDesign;
    [Range(0, 255)]
    public int GameDesign;
    [Range(0, 255)]
    public int SoundDesign;
    [Range(0, 255)]
    public int Social;
    [Range(0, 255)]
    public int Leadership;
}
