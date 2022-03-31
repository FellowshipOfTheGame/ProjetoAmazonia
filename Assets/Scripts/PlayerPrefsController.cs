using UnityEngine;

public class PlayerPrefsController: MonoBehaviour
{
    private const string MasterVolumeKey = "master volume";
    private const string MusicVolumeKey = "music volume";
    private const string SoundEffectsVolumeKey = "sfx volume";
    private const string PlayersCountKey = "players count";

    private const float MinVolume = 0f;
    private const float MaxVolume = 1f;

    public static void SetMasterVolume(float volume)
    {
        if (volume >= MinVolume && volume <= MaxVolume)
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MasterVolumeKey, 1f);
    }
    
    public static void SetMusicVolume(float volume)
    {
        if (volume >= MinVolume && volume <= MaxVolume)
        {
            //Debug.Log($"Music volume set to { volume }");
            PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        }
        else
        {
            Debug.LogError("Music Volume is out of Range");
        }
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
    }

    public static void SetSoundEffectsVolume(float volume)
    {
        if (volume >= MinVolume && volume <= MaxVolume)
        {
            //Debug.Log($"Sound effects volume set to { volume }");
            PlayerPrefs.SetFloat(SoundEffectsVolumeKey, volume);
        }
        else
        {
            Debug.LogError("Sound effects volume is out of Range");
        }
    }

    public static float GetSoundEffectsVolume()
    {
        return PlayerPrefs.GetFloat(SoundEffectsVolumeKey, 1f);
    }
    
    public static void SetPlayersCount(int count)
    {
        PlayerPrefs.SetInt(PlayersCountKey, count);
    }
    
    public static int GetPlayersCount()
    {
        return PlayerPrefs.GetInt(PlayersCountKey, 1);
    }
}
