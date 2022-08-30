using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    [SerializeField] private CharacterFace _silvia;
    [SerializeField] private CharacterFace _january;

    public CharacterFace Silvia => _silvia;
    public CharacterFace January => _january;
}