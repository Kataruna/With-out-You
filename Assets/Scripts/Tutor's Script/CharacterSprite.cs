using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSprite : MonoBehaviour
{
    [SerializeField] private CharacterFace _emily;
    [SerializeField] private CharacterFace _anna;

    public CharacterFace Emily => _emily;
    public CharacterFace Anna => _anna;
}
