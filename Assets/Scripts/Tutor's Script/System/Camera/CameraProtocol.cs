using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraProtocol : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineBrain cinemachineBrain;

    private void Update()
    {
        if (cinemachineBrain.ActiveBlend == null) return;
        
        Transform cameraTransform = mainCamera.transform;
        cameraTransform.position = new Vector3(cameraTransform.position.x, target.position.y, cameraTransform.position.z);
        
        transform.LookAt(cameraTransform);
    }
}
