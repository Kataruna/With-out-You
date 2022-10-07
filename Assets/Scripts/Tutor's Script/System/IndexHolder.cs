using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexHolder : MonoBehaviour
{
    public int Index => index;
    
    [SerializeField] private int index;

    public void SetIndex(int i)
    {
        index = i;
    }
}
