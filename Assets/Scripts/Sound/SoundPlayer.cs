using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundPlayer : MonoBehaviour {
    public static SoundPlayer Instance;
    AudioSource _src;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }
        _src = GetComponent<AudioSource>();  
    }
    void Start () {
        DontDestroyOnLoad(gameObject);
        //transform.parent = Camera.main.transform;
        transform.localPosition = Vector2.zero;
	}

    public void PlayBGM(AudioClip bgm)
    {
        if(_src.clip == null || _src.clip.name != bgm.name || !_src.isPlaying)
        {
            _src.clip = bgm;
            _src.Play();
        }
    }

    public void StopBGM()
    {
        _src.Stop();
    }

    public void PlaySound(AudioClip sfx)
    {
        _src.PlayOneShot(sfx);
    }
}
