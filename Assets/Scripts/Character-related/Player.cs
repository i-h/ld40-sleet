using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance;
    public AudioClip LiftSFX;
     Character _carried;
    Animator _anim;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
        _anim = GetComponent<Animator>();
    }

    public void Recruit(Recruitable person)
    {
        if (_carried != null) return;
        _anim.SetBool("IsCarrying", true);
        person.Available = false;
        WanderAI wander = person.GetComponent<WanderAI>();
        if(wander != null)
        {
            wander.enabled = false;
        }
        person.DisableMovement();
        person.CancelInvoke();
        person.transform.parent = transform;
        person.transform.localPosition = Vector2.up;
        person.transform.localEulerAngles = new Vector3(0, 0, 90);
        person.SortOrderOffset = 2;
        foreach(Collider2D c in person.transform.GetComponents<Collider2D>())
        {
            c.enabled = false;
        }
        person.GetRigidbody().Sleep();
        _carried = person;
        SoundPlayer.Instance.PlaySound(LiftSFX);
    }
    public void PutDownCarried(Team dest)
    {
        if (_carried != null)
        {
            if (dest.AddPerson(_carried))
            {
                try
                {
                    Recruitable person = _carried as Recruitable;
                    int voiceIndex = UnityEngine.Random.Range(0, person.VoiceLines.Length);
                    SoundPlayer.Instance.PlaySound(person.VoiceLines[voiceIndex]);
                } catch (Exception e){}

                _carried = null;
                _anim.SetBool("IsCarrying", false);
            }
        }
    }
}
