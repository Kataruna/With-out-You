using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    #region - Variable Decleration -

    [Header("Setting")]
    [SerializeField] private Playlist playlist;
    [SerializeField] private GameObject trackPrefab;

    [Header("Object Assign")]
    [SerializeField] private Transform playlistHolder;
    [SerializeField] private AudioSource audioSource;

    #endregion

    #region - Unity's Method -
    
    void Awake()
    {
        if(trackPrefab.name != "Song Detail") Debug.LogWarning("Warning Wrong Prefab Detected");
    }

    private void Start()
    {
        ReloadPlaylist();
    }

    #endregion

    #region - Custom Method -

    private void ReloadPlaylist()
    {
        foreach (SongDetail song in playlist.song)
        {
            GameObject trackList = Instantiate(trackPrefab, playlistHolder);

            trackList.name = $"{song.trackName} - {song.artist}";

            trackList.transform.GetChild(0).GetComponent<TMP_Text>().text = song.trackName;
            trackList.transform.GetChild(1).GetComponent<TMP_Text>().text = song.artist;
            
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
    }

    #endregion
    
}
