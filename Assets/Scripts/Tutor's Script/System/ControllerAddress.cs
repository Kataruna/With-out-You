using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAddress : MonoBehaviour
{
    public Dictionary<string, GameObject> ControllersAddress => controllerAddress;
    
    public List<ControllerProperties> controllers = new List<ControllerProperties>();
    private Dictionary<string, GameObject> controllerAddress = new Dictionary<string, GameObject>();
    
    public static ControllerAddress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(Instance);
        
        foreach (ControllerProperties controller in controllers)
        {
            controllerAddress[controller.type.ToString()] = controller.controller;
        }
    }

    public T ReturnComponent<T>(ControllerProperties.Type type)
    {
        Debug.Log(controllerAddress[type.ToString()]);
        return controllerAddress[type.ToString()].GetComponent<T>();
    }
}
