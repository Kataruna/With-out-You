using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Interaction : MonoBehaviour, IInteractable
{
    public Type SelectedType => type;
    public EventRecord EventRecord => eventKey;
    public Dialogue DialogueBlueprint => dialogue;
    public bool ForceInteract => forceInteract;
    public bool IsEnable => _isEnable;
    
    public WorldProperties.Timeline Timeline => timeline;
    public WorldProperties.World WorldLine => worldLine;
    public int WorldOrder => worldOrder;
    
    public UnityEvent OnInteract => OnInteraction;
    
    [SerializeField] private Type type;
    [SerializeField] private EventRecord eventKey;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private bool forceInteract;
    [SerializeField] private bool affectTimeline;
    [SerializeField] private WorldProperties.Timeline timeline;
    [SerializeField] private WorldProperties.World worldLine;
    [SerializeField] private int worldOrder;
    
    [SerializeField] private bool isRequireEvent;
    [SerializeField] private string requireEvent;
    
    [SerializeField] private UnityEvent OnInteraction;

    //Maintenance

    [SerializeField] private InterfaceElement interfaceElement;
    [FormerlySerializedAs("pinIcon"), SerializeField] private SpriteRenderer icon;

    private Controller _input;

    private bool _isEnable;
    
    private PlayerInteraction _playerInteraction;

    public enum Type
    {
        Event,
        Dialogue,
        Maintenance
    }
    
    private void Awake()
    {
        _input = new Controller();
    }

    private void Start()
    {
        if(isRequireEvent) Status(false);
    }

    private void OnValidate()
    {
        UpdateIcon();
    }

    public void EnableInput()
    {
        if (!_isEnable) return;

        _input.Enable();
        Debug.Log($"Input is enabled on {gameObject.name}");
        _input.Player.Interact.performed += _ => UpdateEvent();

        UpdateIcon(true);
    }

    public void DisableInput()
    {
        _input.Player.Interact.performed -= _ => UpdateEvent();
        Debug.Log($"Input is disabled on {gameObject.name}");
        _input.Disable();
        
        UpdateIcon(false);
    }
    
    private void FixedUpdate()
    {
        if(!isRequireEvent || EventHorizon.Instance.EventsHorizon[requireEvent]) Status(true);
    }

    public void Status(bool status)
    {
        _isEnable = status;
        
        if(_isEnable) icon.gameObject.SetActive(true);
        else icon.gameObject.SetActive(false);
    }

    public void UpdateEvent()
    {
        FeedbacksManager.Instance.InteractFeedback.PlayFeedbacks();
        switch (type)
        {
            case Type.Event:
                EventHorizon.Instance.UpdateEvent(eventKey.eventName, eventKey.status);
                OnInteraction.Invoke();
                break;
            case Type.Dialogue:
                DisplayDialog();
                break;
        }
        
        _playerInteraction.ClearInteraction();
        gameObject.SetActive(false);
        DisableInput();
    }

    public void DisplayDialog()
    {
        DialogueDisplay.Instance.EnterDialogue(dialogue);
    }

    public void SetEventKey(string key)
    {
        eventKey.eventName = key;
    }

    public void SetEventValue(bool status)
    {
        eventKey.status = status;
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

    public void SetPlayer(PlayerInteraction player)
    {
        _playerInteraction = player;
    }
}
