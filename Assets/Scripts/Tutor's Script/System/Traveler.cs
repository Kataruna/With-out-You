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

    [SerializeField] private bool doChangeTime;
    [SerializeField] private WorldProperties.Timeline destinationTimeline;
    [SerializeField] private WorldProperties.World destinationWorld;
    
    
    [SerializeField] private InterfaceElement interfaceElement;
    [SerializeField] private SpriteRenderer icon;

    private bool _showIcon = false;
    
    private Controller _input;

    private TARDIS _tardis;
    
    public void Travel()
    {
        if (requireEvent == String.Empty || EventHorizon.Instance.EventsHorizon[requireEvent])
        {
            DisableInput();
            LoadingScreenController.Instance.LoadNextScene($"{nextScene}");
            _tardis.orderOfAppearance = sceneOrder;

            if (doChangeTime)
            {
                _tardis.activeTimeline = destinationTimeline;
                _tardis.activeWorld = destinationWorld;
            }
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
        
        _tardis = TARDIS.Instance;
    }
    
    private void OnValidate()
    {
        UpdateIcon();
    }

    public void EnableInput()
    {
        _input.Enable();
        Debug.Log($"Input is enabled on {gameObject.name}");
        _input.Player.Interact.performed += _ => Travel();
        
        UpdateIcon(true);
    }

    public void DisableInput()
    {
        _input.Player.Interact.performed -= _ => Travel();
        Debug.Log($"Input is disabled on {gameObject.name}");
        _input.Disable();
        
        UpdateIcon(false);
    }

    public void SetRequireEvent(string eventName)
    {
        requireEvent = eventName;
    }
    
    [ContextMenu("Update Icon")]
    public void UpdateIcon()
    {
        if (icon == null) return;
        
        if (forceInteract) icon.sprite = interfaceElement.ForceInteract;
        else icon.sprite = interfaceElement.Normal;
    }
    
    public void UpdateIcon(bool isOnHover)
    {
        if (icon == null) return;
        
        if (forceInteract) icon.sprite = interfaceElement.ForceInteract;
        else
        {
            if (isOnHover) icon.sprite = interfaceElement.OnHover;
            else icon.sprite = interfaceElement.Normal;
        }
    }
    
    
}
