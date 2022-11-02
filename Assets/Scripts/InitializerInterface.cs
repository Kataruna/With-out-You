using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializerInterface : MonoBehaviour
{
    private const string INTERFACE_SCENE_NAME = "Interface";

    private void Awake()
    {
        Scene interfaceScene = SceneManager.GetSceneByName(INTERFACE_SCENE_NAME);

        if(interfaceScene == null || !interfaceScene.isLoaded)
        {
            SceneManager.LoadScene(INTERFACE_SCENE_NAME, LoadSceneMode.Additive);
        }
    }
}
