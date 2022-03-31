using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private AudioManager _audioManager;
    private Slider _musicSlider;
    private Slider _soundEffectsSlider;
    
    private void Awake()
    {
        _audioManager = AudioManager.Instance;
        _musicSlider = GameObject.FindWithTag("MusicSlider").GetComponent<Slider>();
        _soundEffectsSlider = GameObject.FindWithTag("SoundEffectsSlider").GetComponent<Slider>();
        _musicSlider.value = _audioManager.GetMusicVolume();
        _soundEffectsSlider.value = _audioManager.GetSoundEffectsVolume();
    }

    public void OnMusicSlideChange(float value)
    {
        _audioManager.SetMusicVolume(value);
    }
    
    public void OnSoundEffectsChange(float value)
    {
        _audioManager.SetSoundEffectsVolume(value);
    }
}
