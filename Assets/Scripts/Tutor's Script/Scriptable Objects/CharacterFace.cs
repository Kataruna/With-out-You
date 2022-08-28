using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Character Face")]
public class CharacterFace : ScriptableObject
{
    [SerializeField] private Sprite neutral;
    [SerializeField] private Sprite angry;
    [SerializeField] private Sprite sad;
    [SerializeField] private Sprite curious;

    public Sprite Neutral => neutral;
    public Sprite Angry => angry;
    public Sprite Sad => sad;
    public Sprite Curious => curious;
}
