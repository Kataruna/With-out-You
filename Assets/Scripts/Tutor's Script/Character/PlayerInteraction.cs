using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction : Singleton<PlayerInteraction>
{
    [SerializeField] private GameObject activeInteraction;

    private void OnTriggerEnter(Collider other)
    {
        if(activeInteraction != null) return;

        switch (other.tag)
        {
            case "Story":
            case "NPC":
                activeInteraction = other.gameObject;
                activeInteraction.gameObject.TryGetComponent(out Interaction interacted);

                if (interacted.ForceInteract) interacted.UpdateEvent();
                else interacted.EnableInput();
                
                break;
            case "Travel":
                activeInteraction = other.gameObject;
                activeInteraction.gameObject.TryGetComponent(out Traveler travel);
                
                travel.EnableInput();
                
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == activeInteraction)
        {
            activeInteraction.gameObject.TryGetComponent(out Interaction interacted);
            activeInteraction.gameObject.TryGetComponent(out Traveler travel);
                
            if(travel != null) travel.DisableInput();
            if(interacted != null)interacted.DisableInput();
            
            
            activeInteraction = null;
        }
    }

    public void ClearInteraction()
    {
        activeInteraction = null;
    }
}
