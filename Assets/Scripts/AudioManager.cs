using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private BGMData _backgroundMusic;
    private AudioSource _backgroundMusicSource;

    [Range(1, 256)]
    [SerializeField] private int _soundEffectSourceCount = 16; 
    private AudioSource[] _soundEffectsSources;
    private int _currentSoundEffectSource;

    private float _masterVolume = 1f;
    private float _backgroundMusicVolume = 1f;
    private float _soundEffectsVolume = 1f;

    private Coroutine _backgroundMusicHandler = null;

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
    }

    private void Start()
    {
        _masterVolume = GetMasterVolume();
        _backgroundMusicVolume = GetMusicVolume();
        _soundEffectsVolume = GetSoundEffectsVolume();
        
        InitializeMasterVolume();
        InitializeMusicSource();
        InitializeSoundEffectSources();
    }

    public void SetMasterVolume(float value)
    {
        _masterVolume = value;

        AudioListener.volume = _masterVolume;
        PlayerPrefsController.SetMasterVolume(_masterVolume);
    }

    public float GetMasterVolume()
    {
        return PlayerPrefsController.GetMasterVolume();
    }

    public void SetSoundEffectsVolume(float value)
    {
        _soundEffectsVolume = value;
        if(_soundEffectsSources != null) {
            foreach(AudioSource source in _soundEffectsSources) {
                if(!source.isPlaying) {
                    source.volume = value;
                }
            }
        }
        PlayerPrefsController.SetSoundEffectsVolume(value);
    }
    
    public float GetSoundEffectsVolume()
    {
        return PlayerPrefsController.GetSoundEffectsVolume();
    }

    public void SetMusicVolume(float value)
    {
        _backgroundMusicVolume = value;

        if(_backgroundMusicSource != null)
            _backgroundMusicSource.volume = _backgroundMusicVolume;
        PlayerPrefsController.SetMusicVolume(_backgroundMusicVolume);
    }
    
    public float GetMusicVolume()
    {
        return PlayerPrefsController.GetMusicVolume();
    }

    public void SetBackgroundMusic(BGMData backgroundMusic) 
    {
        _backgroundMusic = backgroundMusic;

        if(_backgroundMusicSource == null)
        {
            Debug.LogError($"{this.GetType().Name}: Called SetBackgroundMusic but background music's Audio Source is "
            + "not available!");
            return;
        }

        _backgroundMusicSource.Stop();
        StopCoroutine(_backgroundMusicHandler);

        if(_backgroundMusic != null) {
            _backgroundMusicHandler = StartCoroutine(HandleBackgroundMusic());
        }
    }

    public BGMData GetBackgroundMusic()
    {
        return _backgroundMusic;
    }

    public void PlaySoundEffect(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        if(clip == null)
            return;
            
        if(_soundEffectsSources[_currentSoundEffectSource].isPlaying)
        {
            _soundEffectsSources[_currentSoundEffectSource].Stop();
            Debug.LogWarning($"{this.GetType().Name}: Out of AudioSources! The oldest one will be overwritten! If you "
            + "experience audio clipping please increase _soundEffectSourceCount.");

            // Note that there's a chance we have free AudioSources (if a newer Source had a shorter clip and has
            // already finished) but we can't detect that without using a more complex system =(
        }

        _soundEffectsSources[_currentSoundEffectSource].clip = clip;
        _soundEffectsSources[_currentSoundEffectSource].volume = volume * _soundEffectsVolume;
        _soundEffectsSources[_currentSoundEffectSource].pitch = pitch;
        _soundEffectsSources[_currentSoundEffectSource].Play();

        _currentSoundEffectSource = (_currentSoundEffectSource + 1) % _soundEffectsSources.Length;
    }

    private void InitializeMasterVolume()
    {
        AudioListener.volume = _masterVolume;
    }

    private void InitializeMusicSource()
    {
        _backgroundMusicSource = CreateAudioSource("BackgroundMusicSource", false, _backgroundMusicVolume);

        if(_backgroundMusic != null) {
            _backgroundMusicHandler = StartCoroutine(HandleBackgroundMusic());
        }
    }

    private void InitializeSoundEffectSources()
    {
        _soundEffectsSources = new AudioSource[_soundEffectSourceCount];

        for(int i = 0; i < _soundEffectSourceCount; i++)
            _soundEffectsSources[i] = CreateAudioSource($"SoundEffectSource_{i}", false, _soundEffectsVolume);

        _currentSoundEffectSource = 0;
    }

    private AudioSource CreateAudioSource(string name, bool loop = false, float volume = 1.0f)
    {
        var audioSourceObject = new GameObject(name);
        var audioSource = audioSourceObject.AddComponent<AudioSource>();

        audioSourceObject.transform.SetParent(this.transform);

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0.0f;

        audioSource.loop = loop;
        audioSource.volume = volume;

        return audioSource;
    }

    private IEnumerator HandleBackgroundMusic() {
        
        do {

            if(!_backgroundMusicSource.isPlaying) {
                _backgroundMusicSource.clip = _backgroundMusic.clip;
                _backgroundMusicSource.Play();
                _backgroundMusic = _backgroundMusic.nextBGM;
            }

            yield return null;

        } while(_backgroundMusic != null);

        _backgroundMusicHandler = null;
    }
}
