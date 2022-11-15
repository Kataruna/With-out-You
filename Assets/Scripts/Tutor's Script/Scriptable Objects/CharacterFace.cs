using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Dialog/Character Face")]
public class CharacterFace : ScriptableObject
{
    [FormerlySerializedAs("name")]
    [Header("Name")]
    [SerializeField] private string fullName;
    [SerializeField] private string nickname;
    
    [Header("Face")]
    [SerializeField] private Sprite neutral;
    [SerializeField] private Sprite angry;
    [SerializeField] private Sprite sad;
    [SerializeField] private Sprite curious;
    [SerializeField] private Sprite happy;
    
    public string FullName => fullName;
    public string Nickname => nickname;
    
    public Sprite Neutral => neutral;
    public Sprite Angry => angry;
    public Sprite Sad => sad;
    public Sprite Curious => curious;
    public Sprite Happy => happy;
}