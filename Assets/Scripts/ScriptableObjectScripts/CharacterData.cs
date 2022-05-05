using UnityEngine;

[CreateAssetMenu(fileName = "CharacteraData", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    public float movementMultiplier = 5f;
    public float rotationLerpValue = 6f;
    public int characterCapacity = 30;
    
}
