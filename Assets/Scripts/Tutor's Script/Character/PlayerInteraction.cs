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
            case "NPC":
                activeInteraction = other.gameObject;
                activeInteraction.gameObject.GetComponent<Interaction>().EnableInput();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == activeInteraction)
        {
            activeInteraction.gameObject.GetComponent<Interaction>().DisableInput();
            activeInteraction = null;
        }
    }
}
