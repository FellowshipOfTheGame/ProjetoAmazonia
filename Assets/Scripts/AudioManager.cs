using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioClip _backgroundMusic;
    private AudioSource _backgroundMusicSource;

    [Range(1, 256)]
    [SerializeField] private int _soundEffectSourceCount = 16; 
    private AudioSource[] _soundEffectsSources;
    private int _currentSoundEffectSource;

    private float _backgroundMusicVolume = 1f;
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
    }

    private void Start()
    {
        _backgroundMusicVolume = PlayerPrefsController.GetMusicVolume();
        _soundEffectsVolume = PlayerPrefsController.GetSoundEffectsVolume();
        
        InitializeMusicSource();
        InitializeSoundEffectSources();
    }

    public void SetSoundEffectsVolume(float value)
    {
        _soundEffectsVolume = value;
        if(_soundEffectsSources != null) {
            foreach(AudioSource source in _soundEffectsSources)
                source.volume = value;
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
            _backgroundMusicSource.volume = value;
        PlayerPrefsController.SetMusicVolume(value);
    }
    
    public float GetMusicVolume()
    {
        return PlayerPrefsController.GetMusicVolume();
    }

    public void SetBackgroundMusic(AudioClip backgroundMusic) 
    {
        _backgroundMusic = backgroundMusic;

        if(_backgroundMusicSource == null)
        {
            Debug.LogError($"{this.GetType().Name}: Called SetBackgroundMusic but background music's Audio Source is "
            + "not available!");
            return;
        }

        _backgroundMusicSource.Stop();

        if(backgroundMusic == null)
            return;
        _backgroundMusicSource.clip = backgroundMusic;
        _backgroundMusicSource.Play();
    }

    public AudioClip GetBackgroundMusic()
    {
        return _backgroundMusic;
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if(_soundEffectsSources[_currentSoundEffectSource].isPlaying)
        {
            _soundEffectsSources[_currentSoundEffectSource].Stop();
            Debug.LogWarning($"{this.GetType().Name}: Out of AudioSources! The oldest one will be overwritten! If you "
            + "experience audio cliping please increase _soundEffectSourceCount.");

            // Note that there's a chance we have free AudioSources (if a newer Source had a shorter clip and has
            // already finished) but we can't detect that without using a more complex system =(
        }

        _soundEffectsSources[_currentSoundEffectSource].clip = clip;
        _soundEffectsSources[_currentSoundEffectSource].Play();

        _currentSoundEffectSource = (_currentSoundEffectSource + 1) % _soundEffectsSources.Length;
    }

    private void InitializeMusicSource()
    {
        _backgroundMusicSource = CreateAudioSource("BackgroundMusicSource", true, _backgroundMusicVolume);

        if(_backgroundMusic != null) {
            _backgroundMusicSource.clip = _backgroundMusic;
            _backgroundMusicSource.Play();
        }
    }

    private void InitializeSoundEffectSources()
    {
        _soundEffectsSources = new AudioSource[_soundEffectSourceCount];

        for(int i = 0; i < _soundEffectSourceCount; i++)
            CreateAudioSource($"SoundEffectSource_{i}", false, _soundEffectsVolume);

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
        audioSource.volume = _soundEffectsVolume;

        return audioSource;
    }
}
