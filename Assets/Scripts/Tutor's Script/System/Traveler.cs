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
    [SerializeField, Tooltip("ลำดับการปรากฏของซีนนี้")] private int sceneOrder;
    [SerializeField] private string requireEvent;
    [SerializeField] private GameObject icon;
   
    private bool _showIcon = false;
    
    private Controller _input;
    
    public void Travel()
    {
        if (requireEvent == String.Empty || EventHorizon.Instance.EventsHorizon[requireEvent])
        {
            DisableInput();
            LoadingScreenController.Instance.LoadNextScene($"{nextScene}{sceneOrder}");
        }
    }

    private void FixedUpdate()
    {
        if(requireEvent == String.Empty || EventHorizon.Instance.EventsHorizon[requireEvent]) Status(true);
    }

    public void Status(bool status)
    {
        _showIcon = status;
        
        if(_showIcon) icon.SetActive(true);
        else icon.SetActive(false);
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
