using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("--------AudioSource------")]
    public AudioSource m_Source;
    public AudioSource SFXSource;

    [Header("--------AudioClip-------")]
    public AudioClip backGroundNorm;
    public AudioClip backGroundBoss;
    public AudioClip attack;
    public AudioClip jump;
    public AudioClip dash;
    public AudioClip hit;
    public AudioClip explode;
    public AudioClip onClick;

    [Header("------Setting-------")]
    public Slider musicSlider;
    public Slider SFXSlider;

    private void Awake()
    {
        PlayMusic(backGroundNorm);
    }
    public void setMusicVolume()
    {
        m_Source.volume = musicSlider.value;
    }
    public void setSFXVolume()
    {
        SFXSource.volume = SFXSlider.value;
    }
    public void PlayMusic(AudioClip clip)
    {
        m_Source.clip = clip;
        m_Source.loop = true;
        m_Source.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void ToggleSound()
    {
        if(m_Source.volume == 0 && SFXSource.volume == 0)
        {
            m_Source.volume = 1;
            SFXSource.volume = 1;
        }
        else
        {
            m_Source.volume = 0;
            SFXSource.volume = 0;
        }
    }
}
