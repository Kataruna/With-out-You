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

    public int ActiveIndex => _activeIndex;
    
    [Header("Setting")]
    [SerializeField] private Playlist playlist;
    [SerializeField] private Button trackPrefab;
    [SerializeField] private float fadeTime = 0.5f;

    [Header("Object Assign")]
    [SerializeField] private Transform playlistHolder;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private MusicControl musicControl;
    
    private Color _black;
    private Color _white;

    private int _activeIndex;
    private ButtonClick _activeSong;
    private List<Button> _songList = new List<Button>();
    
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
        int i = 0;
        
        _activeIndex = -1;
        _activeSong = null;
        _songList.Clear();
        
        foreach (SongDetail song in playlist.song)
        {
            Button trackList = Instantiate(trackPrefab, playlistHolder);

            _songList.Add(trackList);

            if(song.artist != "" ) trackList.name = $"{song.trackName} - {song.artist}";
            else trackList.name = $"{song.trackName}";

            trackList.GetComponent<IndexHolder>().SetIndex(i);
            
            ButtonClick thisButton = trackList.GetComponent<ButtonClick>();

            thisButton.SetText(trackList.name);
            thisButton.SetupButton(_black, _white);
            thisButton.SetFadeTime(fadeTime);

            trackList.GetComponent<Button>().onClick.AddListener(() => PlayMusic(song));
            
            i++;
        }
    }

    private void VolumeSetup(float defaultVolume)
    {
        AudioSetting.Instance.SetMaxLoudness(defaultVolume);
    }
    
    private void PlayMusic(SongDetail song)
    {
        SetupSpeaker(song);
        
        musicControl.Play();

        if(_activeSong != null) _activeSong.Press();

        GameObject songButtonItSelf = EventSystem.current.currentSelectedGameObject;
        
        _activeSong = songButtonItSelf.GetComponent<ButtonClick>();
        _activeIndex = songButtonItSelf.GetComponent<IndexHolder>().Index;
        
        _activeSong.Press();
    }

    /// <summary>
    /// Play Music command that use current position and plus or minus to identify song
    /// </summary>
    /// <param name="index">Normally use 1 /n-1 for previous song and +1 for next song</param>
    public void PlayMusic(int index)
    {
        SongDetail song = playlist.song[_activeIndex+index];
        
        SetupSpeaker(song);
        
        musicControl.Play();
        
        if(_activeSong != null) _activeSong.Press();

        _activeSong = _songList[_activeIndex+index].GetComponent<ButtonClick>();
        _activeIndex += index;
        
        _activeSong.Press();
    }

    public void SetupSpeaker(SongDetail song)
    {
        audioSource.clip = song.trackFile;
        audioSource.volume = song.defaultVolume;
        
        VolumeSetup(song.defaultVolume);
    }

    #endregion
    
}
