using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Object[] additiveList;

    public void LoadRequireScene()
    {
        foreach (Object scene in additiveList)
        {
            SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
        }
    }
}
