using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

[DefaultExecutionOrder(-1)]
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Object[] additiveList;

    private void Awake()
    {
        foreach (Object scene in additiveList)
        {
            SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
        }
    }
}
