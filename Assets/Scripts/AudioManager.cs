using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    private AudioSource _audioSource;
    
    private float _musicVolume = 1f;
    private float _soundEffectsVolume = 1f;

    private void Awake()
    {
        #region Singleton
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        #endregion
        
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _musicVolume = PlayerPrefsController.GetMusicVolume();
        _soundEffectsVolume = PlayerPrefsController.GetSoundEffectsVolume();
        _audioSource.volume = _musicVolume;
    }

    public void SetSoundEffectsVolume(float value)
    {
        _soundEffectsVolume = value;
        PlayerPrefsController.SetSoundEffectsVolume(value);
    }
    
    public float GetSoundEffectsVolume()
    {
        return PlayerPrefsController.GetSoundEffectsVolume();
    }

    public void SetMusicVolume(float value)
    {
        _audioSource.volume = value;
        PlayerPrefsController.SetMusicVolume(value);
    }
    
    public float GetMusicVolume()
    {
        return PlayerPrefsController.GetMusicVolume();
    }
}