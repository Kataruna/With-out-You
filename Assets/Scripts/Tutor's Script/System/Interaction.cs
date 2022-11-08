using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour, IInteractable
{
    public Type SelectedType => type;
    public EventRecord EventRecord => eventKey;
    public Dialogue DialogueBlueprint => dialogue;
    public bool ForceInteract => forceInteract;

    public bool AffectTimeline => affectTimeline;
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
    
    [SerializeField] private UnityEvent OnInteraction;

    private Controller _input;

    public enum Type
    {
        Event,
        Dialogue
    }
    
    private void Awake()
    {
        _input = new Controller();
    }

    public void EnableInput()
    {
        _input.Enable();
        Debug.Log($"Input is enabled on {gameObject.name}");
        _input.Player.Interact.performed += _ => UpdateEvent();
    }

    public void DisableInput()
    {
        _input.Player.Interact.performed -= _ => UpdateEvent();
        Debug.Log($"Input is disabled on {gameObject.name}");
        _input.Disable();
    }

    public void UpdateEvent()
    {
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
        
        PlayerInteraction.Instance.ClearInteraction();
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
}
