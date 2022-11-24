using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class FeedbacksManager : MonoBehaviour
{
    public static FeedbacksManager Instance;
    [Header("Feedbacks")]
    [SerializeField] private MMFeedbacks interactFeedback;
    [SerializeField] private MMFeedbacks walkingFeedback;

    public MMFeedbacks InteractFeedback => interactFeedback;
    public MMFeedbacks WalkingFeedback => walkingFeedback;
    
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
