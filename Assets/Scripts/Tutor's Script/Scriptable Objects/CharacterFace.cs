using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Character Face")]
public class CharacterFace : ScriptableObject
{
    [Header("Name")]
    [SerializeField] private string name;
    [SerializeField] private string nickname;
    
    [Header("Face")]
    [SerializeField] private Sprite neutral;
    [SerializeField] private Sprite angry;
    [SerializeField] private Sprite sad;
    [SerializeField] private Sprite curious;
    
    public string Name => name;
    public string Nickname => nickname;
    
    public Sprite Neutral => neutral;
    public Sprite Angry => angry;
    public Sprite Sad => sad;
    public Sprite Curious => curious;
}