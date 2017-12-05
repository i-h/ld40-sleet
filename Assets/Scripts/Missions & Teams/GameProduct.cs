using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProduct : MonoBehaviour {
    const float BASE_DEV_TIME = 15.0f;
    const float SOCIAL_BONUS_PERCENT = 25.0f;
    public Team DevTeam;
    Stats _totalStats;
    Action OnFinishedCallback;
    public float DevelopmentTime;
    public float Quality;
    public float Progress = 0.0f;
	void Update () {
		
	}
    public void StartDevelopment(Team team, Action onReady)
    {
        team.GameProject = this;
        DevTeam = team;
        OnFinishedCallback = onReady;
        _totalStats = GetTotalStats();

        DevelopmentTime = GetDevelopmentTime();
        Quality = GetQuality();

        StartCoroutine(DevelopmentCycle());
    }
    IEnumerator DevelopmentCycle()
    {
        Debug.Log("Game started with " + DevelopmentTime + " ("+_totalStats.Leadership+" leadership) with " + Quality + "% quality");
        float t = 0;
        while(Progress < 1.0)
        {


            t += Time.deltaTime;
            Progress = t / DevelopmentTime;
            yield return null;
        }
        Debug.Log("Game finished in " + DevelopmentTime + " with " + Quality + "% quality");
        OnFinishedCallback();
    }

    Stats GetTotalStats()
    {
        Stats s = new Stats();

        foreach(TeamSlot ts in DevTeam.TeamSlots)
        {
            Recruitable person = ts.Person as Recruitable;
            if(person != null)
            {
                Stats pStats = person.Attributes;
                s.Programming += pStats.Programming;
                s.GraphicDesign += pStats.GraphicDesign;
                s.GameDesign += pStats.GameDesign;
                s.SoundDesign += pStats.SoundDesign;
                s.Social += pStats.Social;
                s.Leadership += pStats.Leadership;
            }
        }

        return s;
    }

    float GetDevelopmentTime()
    {
        float leadership = _totalStats.Leadership;
        float baseDuration = BASE_DEV_TIME;
        if(leadership > 255)
        {
            return baseDuration + baseDuration * ((255/leadership/2)-1);
        } else
        {
            return baseDuration + baseDuration * (1-leadership/255);
        }
    }

    float GetQuality()
    {
        float socialBonus = SOCIAL_BONUS_PERCENT;
        float q = (_totalStats.Programming + _totalStats.GraphicDesign + _totalStats.GameDesign + _totalStats.SoundDesign) / 4 / 2.55f;
        q += (_totalStats.Social / DevTeam.TeamSlots.Count / 255 * socialBonus);
        return q;
    }
}
