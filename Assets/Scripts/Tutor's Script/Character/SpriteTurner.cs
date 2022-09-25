using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTurner : MonoBehaviour
{
    [SerializeField] private Camera activeCamera;
    
    void FixedUpdate()
    {
        transform.LookAt(activeCamera.transform);
    }
}
