using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip LoseMusic;

    private void Awake()
    {
        PlayMenuMusic();
    }

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Play A Music That you Give it By Parameter
    /// </summary>
    /// <param name="clip"></param>
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    /// <summary>
    /// Stops The Current Music Audio Clip
    /// </summary>
    public void StopMusic()
    {
        musicSource.Stop();
    }

    /// <summary>
    /// Returns The Audio Source For The Music
    /// </summary>
    /// <returns></returns>
    public AudioSource GetMusicSource()
    {
        return musicSource;
    }

    public void PlayMenuMusic()
    {
        musicSource.Stop();
        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void PlayGameplayMusic()
    {
        musicSource.Stop();
        musicSource.clip = gameplayMusic;
        musicSource.Play();
    }

    public void PlayLoseMusic()
    {
        musicSource.Stop();
        musicSource.clip = LoseMusic;
        musicSource.Play();
    }
}