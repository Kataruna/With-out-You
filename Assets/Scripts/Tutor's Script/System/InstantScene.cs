using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantScene : MonoBehaviour
{
    public void ToScene(string sceneName)
    {
        LoadingScreenController.Instance.LoadNextScene(sceneName);
    }
}
