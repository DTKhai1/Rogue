using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("--------AudioSource------")]
    [SerializeField] AudioSource m_Source;
    [SerializeField] AudioSource SFXSource;

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
        m_Source.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void TurnOffSound()
    {
        m_Source.volume = 0;
        SFXSource.volume = 0;
    }
}
