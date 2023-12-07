using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sounds.Scripts;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private AudioSource sissySource;
    
    [SerializeField]
    private AudioSource rockSource;
    
    [SerializeField]
    private AudioSource uiSource;
    
    [SerializeField]
    private Audio_Data_Bundle audioDataBundle;
    
    private void Awake()
    {
        if (musicSource == null)
        {
           musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        }
        
        if (sfxSource == null)
        {
            sfxSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
        }
        
        if (sissySource == null)
        {
            sissySource = GameObject.Find("SissySource").GetComponent<AudioSource>();
        }
        
        if (rockSource == null)
        {
            rockSource = GameObject.Find("RockSource").GetComponent<AudioSource>();
        }
        
        if (uiSource == null)
        {
            uiSource = GameObject.Find("UISource").GetComponent<AudioSource>();
        }
        
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
            case SourceAudioType.Sissy:
                PlaySissy(data);
                break;
            case SourceAudioType.Rock:
                PlayRock(data);
                break;
        }
        
        uiSource.loop = false;
    }
    
    private void PlayMusic(Audio_Data data)
    {
        if (data.shouldOverride)
        {
            StopMusic();
        }

        if (!musicSource.isPlaying)
        {
            musicSource.clip = data.clip.Count > 1 ? data.clip[Random.Range(0, data.clip.Count)] : data.clip[0];
        
            musicSource.volume = data.volume;
            musicSource.Play();
        }
    }
    
    private void PlaySFX(Audio_Data data)
    {
        if (data.shouldOverride)
        {
            StopSFX();
        }

        sfxSource.clip = data.clip.Count > 1 ? data.clip[Random.Range(0, data.clip.Count)] : data.clip[0];
        
        sfxSource.volume = data.volume;
        sfxSource.Play();
    }
    
    private void PlayUI(Audio_Data data)
    {
        if (data.shouldOverride)
        {
            StopUI();
        }

        if (uiSource.isPlaying != false)
        {
            uiSource.clip = data.clip.Count > 1 ? data.clip[Random.Range(0, data.clip.Count)] : data.clip[0];
            uiSource.volume = data.volume;
            uiSource.Play();
        }
    }
    
    private void PlaySissy(Audio_Data data)
    {
        if (data.shouldOverride)
        {
            StopSissy();
        }
        
        sissySource.clip = data.clip.Count > 1 ? data.clip[Random.Range(0, data.clip.Count)] : data.clip[0] ;
        sissySource.volume = data.volume;
        sissySource.Play();
    }
    
    private void PlayRock(Audio_Data data)
    {
        if (data.shouldOverride)
        {
            StopRock();
        }
        
        rockSource.clip = data.clip.Count > 1 ? data.clip[Random.Range(0, data.clip.Count)] : data.clip[0];
        rockSource.volume = data.volume;
        rockSource.Play();
    }
    
    private void StopSissy()
    {
        sissySource.Stop();
    }
    
    private void StopRock()
    {
        rockSource.Stop();
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
        musicSource.loop = true;
        PlayMusic(audioDataBundle.MusicData);
    
        sissySource.loop = true;
        PlaySissy(audioDataBundle.SissyData);
        
        rockSource.loop = true;
        PlayRock(audioDataBundle.RockData);
        
        sfxSource.loop = true;
    }
    
}


