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
        musicSource.clip = data.clip;
        musicSource.volume = 1f;
        musicSource.Play();
    }
    
    private void PlaySFX(Audio_Data data)
    {
        sfxSource.clip = data.clip;
        sfxSource.Play();
    }
    
    private void StopSFX()
    {
        sfxSource.Stop();
    }
    
    private void PlayUI(Audio_Data data)
    {
        uiSource.clip = data.clip;
        uiSource.Play();
    }
    
    public void PlayBasedSounds()
    {
        musicSource.clip = audioDataBundle.MusicData.clip;
        musicSource.loop = true;
        musicSource.volume = 0.3f;
        musicSource.Play();
        
        sfxSource.clip = audioDataBundle.SFXData.clip;
        sfxSource.loop = true;
        sfxSource.Play();
    }
    
}


