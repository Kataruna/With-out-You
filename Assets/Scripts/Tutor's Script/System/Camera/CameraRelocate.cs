using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraRelocate : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcamOperator;

    private void Start()
    {
        if(vcamOperator == null) gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            vcamOperator.Priority = 11;
            other.TryGetComponent(out CameraProtocol player);
            
            player.Focus(false, vcamOperator);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            vcamOperator.Priority = 0;
            
            other.TryGetComponent(out CameraProtocol player);
            
            player.Focus(true, vcamOperator);
        }
    }
}
