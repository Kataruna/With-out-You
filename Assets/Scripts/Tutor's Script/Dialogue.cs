using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Dialogue Blueprint")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private DialogueProperties[] _dialogue;

    public DialogueProperties[] dialogue => _dialogue;
}