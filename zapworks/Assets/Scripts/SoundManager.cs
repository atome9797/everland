using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audioClips;

    public static SoundManager _instance;

    public AudioSource bgSound;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    public void PlaySound(string soundName)
    {
        bgSound.Stop();


        for(int i = 0; i< audioClips.Length; i++)
        {
            if(audioClips[i].name == soundName)
            {
                BgSoundPlay(audioClips[i]);
            }
        }

    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.9f;
        bgSound.Play();
    }
}
