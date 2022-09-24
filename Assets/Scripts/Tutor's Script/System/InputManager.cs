using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;

    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;
    
    private Controller _inputController;

    private void Awake()
    {
        _inputController = new Controller();
    }

    private void OnEnable()
    {
        _inputController.Enable();
        
        TouchSimulation.Enable();
        
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        _inputController.Disable();
        
        TouchSimulation.Disable();
        
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
        EnhancedTouchSupport.Disable();
    }

    private void Start()
    {
        _inputController.Touch.TouchPress.started += ctx => StartTouch(ctx);
        _inputController.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Touch Detected");
        Debug.Log($"Touch started {_inputController.Touch.TouchPosition.ReadValue<Vector2>()}");
        if (OnStartTouch != null)
        {
            OnStartTouch(_inputController.Touch.TouchPosition.ReadValue<Vector2>(), (float)callbackContext.startTime);
        }
    }

    private void EndTouch(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("End Touch");
        if (OnEndTouch != null)
        {
            OnEndTouch(_inputController.Touch.TouchPosition.ReadValue<Vector2>(), (float)callbackContext.time);
        }
    }

    private void FingerDown(Finger finger)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(finger.screenPosition, Time.time);
        }
    }
}
