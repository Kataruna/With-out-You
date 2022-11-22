using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraRelocate : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcamOperator;
    [SerializeField] private CameraProtocol.Direction focusDirection;
    [SerializeField, Tooltip("Change only in case of facing wrong Direction")] private CameraProtocol.Direction defocusDirection = CameraProtocol.Direction.North;

    private bool _focusIsNorth;
    private bool _focusIsEast;
    
    private bool _defocusIsNorth;
    private bool _defocusIsEast;

    private void Start()
    {
        if(vcamOperator == null) gameObject.SetActive(false);
        
        DirectionConverter(focusDirection, out _focusIsNorth, out _focusIsEast);
        DirectionConverter(defocusDirection, out _defocusIsNorth, out _defocusIsEast);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            vcamOperator.Priority = 11;
            other.TryGetComponent(out CameraProtocol player);
            
            player.Focus(_focusIsNorth, _focusIsEast);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            vcamOperator.Priority = 0;
            
            other.TryGetComponent(out CameraProtocol player);
            
            player.Focus(_defocusIsNorth, _defocusIsEast);
        }
    }

    private void DirectionConverter(CameraProtocol.Direction direction, out bool isNorth, out bool isRight)
    {
        isNorth = false;
        isRight = false;
        
        switch (direction)
        {
            case CameraProtocol.Direction.North:
                isNorth = true;
                isRight = true;
                break;
            case CameraProtocol.Direction.East:
                isNorth = false;
                isRight = true;
                break;
            case CameraProtocol.Direction.South:
                isNorth = true;
                isRight = false;
                break;
            case CameraProtocol.Direction.West:
                isNorth = false;
                isRight = false;
                break;
        }
    }
}
