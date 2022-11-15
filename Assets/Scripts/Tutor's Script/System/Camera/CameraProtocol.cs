using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraProtocol : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private float facingTime = 10f;
    [SerializeField] private float detectionZone = 30f;

    private bool _isMainPosition;

    public void Focus(bool state, CinemachineVirtualCamera operatorCamera)
    {
        _isMainPosition = state;
        
        StartCoroutine(SwitchDirection(operatorCamera));
    }

    IEnumerator SwitchDirection(CinemachineVirtualCamera operatorCamera)
    {
        float _time = 0;
        do
        {
            _time += Time.deltaTime;
            
            Transform cameraTransform = cinemachineBrain.transform;
            cameraTransform.position = new Vector3(cameraTransform.position.x, target.position.y, cameraTransform.position.z);

            target.transform.LookAt(cinemachineBrain.transform);
            yield return 0;
        } while (_time < facingTime);
        
        if(_isMainPosition) target.rotation = Quaternion.Euler(mainCamera.transform.rotation.eulerAngles);
        else target.rotation = Quaternion.Euler(operatorCamera.transform.rotation.eulerAngles);

    }
}
