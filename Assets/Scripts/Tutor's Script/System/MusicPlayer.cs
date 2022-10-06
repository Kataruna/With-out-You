using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    #region - Variable Decleration -

    [Header("Setting")]
    [SerializeField] private Playlist playlist;
    [SerializeField] private Button trackPrefab;
    [SerializeField] private float fadeTime = 0.5f;

    [Header("Object Assign")]
    [SerializeField] private Transform playlistHolder;
    [SerializeField] private AudioSource audioSource;
    
    private Color _black;
    private Color _white;

    private ButtonClick _activeSong;
    
    #endregion

    #region - Unity's Method -
    
    void Awake()
    {
        if(trackPrefab.name != "Song") Debug.LogWarning("Warning Wrong Prefab Detected");
    }

    private void Start()
    {
        _white = trackPrefab.GetComponent<Image>().color;
        _black = trackPrefab.transform.GetChild(0).GetComponent<TMP_Text>().color;
        
        ReloadPlaylist();
    }

    #endregion

    #region - Custom Method -

    private void ReloadPlaylist()
    {
        _activeSong = null;
        
        foreach (SongDetail song in playlist.song)
        {
            Button trackList = Instantiate(trackPrefab, playlistHolder);

            if(song.artist != "" ) trackList.name = $"{song.trackName} - {song.artist}";
            else trackList.name = $"{song.trackName}";

            ButtonClick thisButton = trackList.GetComponent<ButtonClick>();
            
            thisButton.SetText(trackList.name);
            thisButton.SetupButton(_black, _white);
            thisButton.SetFadeTime(fadeTime);

            trackList.GetComponent<Button>().onClick.AddListener(() => PlayMusic(song));
        }
    }

    private void VolumeSetup(float defaultVolume)
    {
        AudioSetting.Instance.SetMaxLoudness(defaultVolume);
    }
    
    private void PlayMusic(SongDetail song)
    {
        audioSource.clip = song.trackFile;
        audioSource.volume = song.defaultVolume;
        
        VolumeSetup(song.defaultVolume);
        
        audioSource.Play();

        if(_activeSong != null)_activeSong.Press();
        
        _activeSong = EventSystem.current.currentSelectedGameObject.GetComponent<ButtonClick>();
        _activeSong.Press();
    }

    #endregion
    
}
