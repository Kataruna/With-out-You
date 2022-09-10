using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTurner : MonoBehaviour
{
    [SerializeField] private Camera camera;
    
    void FixedUpdate()
    {
        transform.LookAt(camera.transform);
    }
}
