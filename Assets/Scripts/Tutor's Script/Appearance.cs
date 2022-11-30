using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance : MonoBehaviour
{
    [SerializeField] private GameObject controlledGameObject;
    [SerializeField] private string eventKey;
    [SerializeField] private bool defaultAppearance;

    private void FixedUpdate()
    {
        if (EventHorizon.Instance.EventsHorizon[eventKey])
        {
            switch (defaultAppearance)
            {
                case true:
                    if (controlledGameObject.TryGetComponent(out MeshRenderer meshT))
                    {
                        meshT.enabled = false;
                    }
                    else
                    {
                        controlledGameObject.SetActive(false);
                    }
                    break;
                case false:
                    if (controlledGameObject.TryGetComponent(out MeshRenderer meshF))
                    {
                        meshF.enabled = true;
                    }
                    else
                    {
                        controlledGameObject.SetActive(true);
                    }
                    break;
            }

            enabled = false;
        }
    }
}
