using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveler : MonoBehaviour
{
    public enum Scene
    {
        Library,
        Park,
        Graveyard
    }
    
    [SerializeField] private Scene nextScene;
    [SerializeField] private string requireEvent;
    
    private Controller _input;
    
    public void Travel()
    {
        if(EventHorizon.Instance.EventsHorizon[requireEvent] || requireEvent == String.Empty) LoadingScreenController.Instance.LoadNextScene(nextScene.ToString());
    }
    
    private void Awake()
    {
        _input = new Controller();
    }

    public void EnableInput()
    {
        _input.Enable();
        Debug.Log($"Input is enabled on {gameObject.name}");
        _input.Player.Interact.performed += _ => Travel();
    }

    public void DisableInput()
    {
        _input.Player.Interact.performed -= _ => Travel();
        Debug.Log($"Input is disabled on {gameObject.name}");
        _input.Disable();
    }
}
