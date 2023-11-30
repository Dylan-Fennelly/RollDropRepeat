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

    public void Play(Audio_Data data)
    {
        switch (data.type)
        {
            case SourceAudioType.Music:
                PlayMusic(data);
                break;
            case SourceAudioType.SFX:
                PlaySFX(data);
                break;
            case SourceAudioType.UI:
                PlayUI(data);
                break;
        }
    }
    
    private void PlayMusic(Audio_Data data)
    {
        if (data.clip == null)
        {
            StopMusic();
        }
        else
        {
            musicSource.clip = data.clip;
            musicSource.volume = 1f;
            musicSource.Play();
        }
    }
    
    private void PlaySFX(Audio_Data data)
    {
        if (data.clip == null)
        {
            StopSFX();
        }
        else
        {
            sfxSource.clip = data.clip;
            sfxSource.Play();
        }
        
    }
    
    private void PlayUI(Audio_Data data)
    {
        if (data.clip == null)
        {
            StopUI();
        }
        else
        {
            uiSource.clip = data.clip;
            uiSource.Play();
        }
    }
    
    private void StopSFX()
    {
        sfxSource.Stop();
    }
    
    private void StopMusic()
    {
        musicSource.Stop();
    }
    
    private void StopUI()
    {
        uiSource.Stop();
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


