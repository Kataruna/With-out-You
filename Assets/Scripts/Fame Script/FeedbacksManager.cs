using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class FeedbacksManager : MonoBehaviour
{
    public static FeedbacksManager Instance;
    [Header("Feedbacks")]
    [SerializeField] private MMFeedbacks interactFeedback;
    

    public MMFeedbacks InteractFeedback => interactFeedback;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
