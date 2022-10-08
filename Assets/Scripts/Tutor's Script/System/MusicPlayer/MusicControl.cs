using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public bool IsPlay => isPlay;

    [SerializeField] private float previousCooldown = 1f;
    [SerializeField] private bool isPlay;
    [SerializeField] private AudioSource musicPlay;
    [SerializeField] private MusicPlayer player;

    private bool _isInPreviousCooldown;
    private IEnumerator _cooldown;
    private float _timer;

    private void FixedUpdate()
    {
        if(_timer >= previousCooldown) return;

        _timer += Time.deltaTime;

        if (_timer >= previousCooldown) _isInPreviousCooldown = false;
    }

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

        _isInPreviousCooldown = true;
        _timer = 0f;
    }

    public void Next()
    {
        player.PlayMusic(1);
    }

    public void Previous()
    {
        if (_isInPreviousCooldown)
        {
            Debug.Log("Previous");
            player.PlayMusic(-1);
        }
        else
        {
            Debug.Log("Back");
            musicPlay.Stop();
            musicPlay.Play();

            _isInPreviousCooldown = true;
            _timer = 0;
        }
    }
}
