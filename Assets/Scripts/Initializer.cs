using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    private const string LOADING_SCENE_NAME = "Loading";

    private void Awake()
    {
        Scene loadingScreenScene = SceneManager.GetSceneByName(LOADING_SCENE_NAME);
        if (loadingScreenScene == null || !loadingScreenScene.isLoaded)
        {
            SceneManager.LoadScene(LOADING_SCENE_NAME, LoadSceneMode.Additive);
        }
    }
}
