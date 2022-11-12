using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Presentation/Interface Element")]
public class InterfaceElement : ScriptableObject
{
    public Sprite Interact;
    public Sprite InteractHover;
    public Sprite ForceInteract;
    public Sprite ForceInteractHover;

    public Sprite SwitchScene;
    public Sprite SwitchSceneHover;
    public Sprite ForceSwitchScene;
    public Sprite ForceSwitchSceneHover;
}
