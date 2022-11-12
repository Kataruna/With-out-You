using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveler : MonoBehaviour
{
    public enum Mode
    {
        Edit,
        Maintenance,
    }
    
    public enum Scene
    {
        Library,
        Park,
        Graveyard
    }

    public Mode SelectedMode => mode;
    public Scene NextScene => nextScene;
    public string RequireEvent => requireEvent;
    
    public bool ForceInteract => forceInteract;
    
    [SerializeField] private Mode mode;
    [SerializeField] private Scene nextScene;
    [SerializeField, Tooltip("ลำดับการปรากฏของซีนนี้")] private int sceneOrder;
    
    [SerializeField] private bool forceInteract;
    
    [SerializeField] private bool isRequireEvent;
    [SerializeField] private string requireEvent;
    
    [SerializeField] private InterfaceElement interfaceElement;
    [SerializeField] private SpriteRenderer icon;
    
   
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
        
        if(_showIcon) icon.gameObject.SetActive(true);
        else icon.gameObject.SetActive(false);
    }

    private void Awake()
    {
        _input = new Controller();
    }

    private void Start()
    {
        if (forceInteract) icon.sprite = interfaceElement.ForceInteract;
    }

    public void EnableInput()
    {
        _input.Enable();
        Debug.Log($"Input is enabled on {gameObject.name}");
        _input.Player.Interact.performed += _ => Travel();
        
        if(!forceInteract) icon.sprite = interfaceElement.OnHover;
    }

    public void DisableInput()
    {
        _input.Player.Interact.performed -= _ => Travel();
        Debug.Log($"Input is disabled on {gameObject.name}");
        _input.Disable();
        
        if(!forceInteract) icon.sprite = interfaceElement.Normal;
    }

    public void SetRequireEvent(string eventName)
    {
        requireEvent = eventName;
    }
    
    [ContextMenu("Update Icon")]
    public void UpdateIcon()
    {
        if (forceInteract) icon.sprite = interfaceElement.ForceInteract;
        else icon.sprite = interfaceElement.Normal;
    }
}
