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
                    controlledGameObject.SetActive(false);
                    break;
                case false:
                    controlledGameObject.SetActive(true);
                    break;
            }

            enabled = false;
        }
    }
}
