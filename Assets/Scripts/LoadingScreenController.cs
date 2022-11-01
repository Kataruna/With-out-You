using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    private static LoadingScreenController _instance;
    public static LoadingScreenController Instance => _instance;

    [SerializeField] private GameObject _loadingScreenObject;
    
    private void Awake()
    {
        _instance = this;
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        _loadingScreenObject.SetActive(true);
        
        
        //unload active screen
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        while (!unloadOp.isDone)
        {
            yield return null;
        }
        
        //Load new scene
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!loadOp.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        _loadingScreenObject.SetActive(false);
    }

    public void LoadNextScene(string screenName)
    {
        StartCoroutine(LoadSceneCoroutine(screenName));
    }
}
