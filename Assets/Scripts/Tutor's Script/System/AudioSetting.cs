using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    #region - Variable Decleration -

    [SerializeField, Range(0f, 1f)] private float volume;
    
    private float _maxAudioVolume;

    public static AudioSetting Instance;

    #endregion

    #region - Unity's Method -

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }

    #endregion

    #region - Custom Method -

    public void SetMaxLoudness(float maxVolume)
    {
        _maxAudioVolume = maxVolume;
        
        VolumeUpdate();
    }

    public void VolumeUpdate()
    {
        float convertedVolume = volume * _maxAudioVolume;
        ControllerAddress.Instance.ReturnComponent<AudioSource>(ControllerProperties.Type.AudioSource).volume = convertedVolume;
    }

    #endregion
}
