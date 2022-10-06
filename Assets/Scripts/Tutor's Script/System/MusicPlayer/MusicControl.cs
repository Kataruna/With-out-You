using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    [SerializeField] private bool isPlay;
    [SerializeField] private AudioSource musicPlay;

    public void PlayButton()
    {
        isPlay = !isPlay;
        
        if(isPlay) musicPlay.Play();
        else musicPlay.Pause();
    }
}
