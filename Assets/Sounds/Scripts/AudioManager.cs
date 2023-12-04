using System.Collections;
using System.Collections.Generic;
using Sounds.Scripts;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioSource uiSource;
    
    [SerializeField]
    private Audio_Data_Bundle audioDataBundle;
    
    private void Awake()
    {
        PlayBasedSounds();
    }
    
    public void Play(Audio_Data_Bundle data)
    {
        if (data.MusicData)
        {
            PlayMusic(data.MusicData);
        }
        if (data.SFXData)
        {
            PlaySFX(data.SFXData);
        }
        else
        {
            StopSFX();
        }
        if (data.UIData)
        {
            PlayUI(data.UIData);
        }
    }
    
    private void PlayMusic(Audio_Data data)
    {
        musicSource.clip = data.clip[0];
        musicSource.volume = data.volume;
        musicSource.Play();
    }
    
    private void PlaySFX(Audio_Data data)
    {
        sfxSource.clip = data.clip[0];
        sfxSource.volume = data.volume;
        sfxSource.Play();
    }
    
    private void StopSFX()
    {
        sfxSource.Stop();
    }
    
    private void PlayUI(Audio_Data data)
    {
        uiSource.clip = data.clip[0];
        uiSource.volume = data.volume;
        uiSource.Play();
    }
    
    public void PlayBasedSounds()
    {
        musicSource.clip = audioDataBundle.MusicData.clip[0];
        musicSource.loop = true;
        musicSource.volume = audioDataBundle.MusicData.volume;
        musicSource.Play();
        
        sfxSource.clip = audioDataBundle.SFXData.clip[0];
        sfxSource.loop = true;
        sfxSource.Play();
    }
    
}


