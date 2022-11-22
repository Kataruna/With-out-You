using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject activeInteraction;

    private void OnTriggerEnter(Collider other)
    {
        if(activeInteraction != null) return;

        switch (other.tag)
        {
            case "Story":
            case "NPC":
                other.TryGetComponent(out Interaction interacted);
                
                if(!interacted.IsEnable) return;
                
                activeInteraction = interacted.gameObject;
                
                interacted.SetPlayer(this);

                if (interacted.ForceInteract) interacted.UpdateEvent();
                else interacted.EnableInput();
                
                break;
            case "Travel":
                other.gameObject.TryGetComponent(out Traveler travel);
                
                if(!travel.IsEnable) return;

                activeInteraction = travel.gameObject;
                
                travel.SetPlayer(this);

                if(travel.ForceInteract) travel.Travel();
                else travel.EnableInput();

                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == activeInteraction)
        {
            activeInteraction.gameObject.TryGetComponent(out Interaction interacted);
            activeInteraction.gameObject.TryGetComponent(out Traveler travel);

            if (travel != null)
            {
                travel.DisableInput();
                travel.SetPlayer(null);
            }

            if (interacted != null)
            {
                interacted.DisableInput();
                interacted.SetPlayer(null);
            }

            activeInteraction = null;
        }
    }

    public void ClearInteraction()
    {
        activeInteraction = null;
    }
}
