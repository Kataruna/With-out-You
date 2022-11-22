using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    
    public void SetMasterVolume(float volume)
    {
        mixer.SetFloat("Master", volume);
    }
    
    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("Music", volume);
    }
    
    public void SetSoundEffectVolume(float volume)
    {
        mixer.SetFloat("SFX", volume);
    }
    
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
