using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour, IInteractable
{
    [SerializeField] private EventRecord eventKey;
    private Controller _input;

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

    private void UpdateEvent()
    {
        EventHorizon.Instance.UpdateEvent(eventKey.eventName, eventKey.status);
    }
}
