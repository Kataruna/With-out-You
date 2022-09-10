using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cinemachine.Editor;
using UnityEngine;

public class CharacterCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera southCamera;
    [SerializeField] private CinemachineVirtualCamera northCamera;
    [SerializeField] private CinemachineVirtualCamera westCamera;
    [SerializeField] private CinemachineVirtualCamera eastCamera;

    private enum Direction
    {
        north,
        south,
        west,
        east,
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) SetMainCamera(Direction.north);
        else if (Input.GetKeyDown(KeyCode.K)) SetMainCamera(Direction.south);
        else if (Input.GetKeyDown(KeyCode.J)) SetMainCamera(Direction.west);
        else if (Input.GetKeyDown(KeyCode.L)) SetMainCamera(Direction.east);
    }

    private void SetMainCamera(Direction direction)
    {
        switch (direction)
        {
            case Direction.east:
                northCamera.Priority = 10;
                southCamera.Priority = 10;
                eastCamera.Priority = 11;
                westCamera.Priority = 10;
                break;
            case Direction.west:
                northCamera.Priority = 10;
                southCamera.Priority = 10;
                eastCamera.Priority = 10;
                westCamera.Priority = 11;
                break;
            case Direction.north:
                northCamera.Priority = 11;
                southCamera.Priority = 10;
                eastCamera.Priority = 10;
                westCamera.Priority = 10;
                break;
            case Direction.south:
                northCamera.Priority = 10;
                southCamera.Priority = 11;
                eastCamera.Priority = 10;
                westCamera.Priority = 10;
                break;
        }
    }
}
