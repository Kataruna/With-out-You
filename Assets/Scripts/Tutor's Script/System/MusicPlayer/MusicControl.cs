using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    [SerializeField] private float previousCooldown = 1f;
    [SerializeField] private bool isPlay;
    [SerializeField] private AudioSource musicPlay;
    [SerializeField] private MusicPlayer player;

    private bool _isInPreviousCooldown;

    public void PlayButton()
    {
        isPlay = !isPlay;
        
        if(isPlay) musicPlay.Play();
        else musicPlay.Pause();
    }
    
    public void Play()
    {
        isPlay = true;
        
        musicPlay.Play();

        StartCoroutine(Cooldown());
    }

    public void Next()
    {
        
    }

    public void Previous()
    {
        if(_isInPreviousCooldown){}
        else
        {
            musicPlay.Stop();
            musicPlay.Play();
        }
    }

    IEnumerator Cooldown()
    {
        float timer = 0;
        _isInPreviousCooldown = true;
        
        while (timer < previousCooldown)
        {
            timer += Time.deltaTime;
        }

        _isInPreviousCooldown = false;
        
        yield return null;
    }
}
