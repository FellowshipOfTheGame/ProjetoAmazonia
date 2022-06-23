using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private Slider _musicSlider;
    private Slider _soundEffectsSlider;
    
    private void Awake()
    {
        _musicSlider = GameObject.FindWithTag("MusicSlider").GetComponent<Slider>();
        _soundEffectsSlider = GameObject.FindWithTag("SoundEffectsSlider").GetComponent<Slider>();
        _musicSlider.value = AudioManager.Instance.GetMusicVolume();
        _soundEffectsSlider.value = AudioManager.Instance.GetSoundEffectsVolume();
    }

    public void OnMusicSlideChange(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }
    
    public void OnSoundEffectsChange(float value)
    {
        AudioManager.Instance.SetSoundEffectsVolume(value);
    }
}
