using UnityEngine;

[CreateAssetMenu(fileName = "Player Health Data", menuName = "Player Data/Player Health Data")]
public class PlayerHealthData : ScriptableObject
{
    public float maxHealth;
    public float godmodeDuration;
}
