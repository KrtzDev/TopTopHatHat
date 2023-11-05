using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : Singleton<SFXManager>
{
    [System.Serializable]
    public class SFX
    {
        public string name;
        public AudioClip audioClip;
        [Range(0.00f, 1.00f)] public float volume = 1;
        [Range(0.00f, 2.00f)] public float pitch = 1;

        public SFX(string _name, AudioClip _audioClip, float _volume, float _pitch)
        {
            name = _name;
            audioClip = _audioClip;
            volume = _volume;
            pitch = _pitch;
        }
    }

    public SFX[] sfxSounds;
    public AudioSource sfxSource;

    public void PlaySound(string name)
    {
        SFX s = Array.Find(sfxSounds, x => x.name == name);
        if(s == null)
        {
            Debug.Log(name + " not found!");
        }
        else
        {
            sfxSource.volume = s.volume;
            sfxSource.PlayOneShot(s.audioClip);
        }
    }
}
