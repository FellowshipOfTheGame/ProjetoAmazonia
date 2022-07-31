using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;

    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _soundEffectsSlider;
    
    private void OnEnable()
    {
        _masterSlider.value = AudioManager.Instance.GetMasterVolume();
        _musicSlider.value = AudioManager.Instance.GetMusicVolume();
        _soundEffectsSlider.value = AudioManager.Instance.GetSoundEffectsVolume();
    }

    public void OnMasterSliderChange(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }

    public void OnMusicSliderChange(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }
    
    public void OnSoundEffectsSliderChange(float value)
    {
        AudioManager.Instance.SetSoundEffectsVolume(value);
    }
    
    public void CloseOptions()
    {
        gameObject.SetActive(false);
        pauseCanvas.SetActive(true);
    }
}
