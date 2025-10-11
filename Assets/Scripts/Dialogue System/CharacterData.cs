using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string speakerID;
    public string displayName;
    public Sprite avatar;
}
