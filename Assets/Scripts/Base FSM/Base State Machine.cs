using UnityEngine;

public enum BASE_STATE
{
    Idle,
    Move,
    Attack,
    Skill,
    Guard,
    Hit,
    Dead
}

public class BaseStateMachine : MonoBehaviour
{
    public float attackDamage = 10f;
    public float skillDamage = 20f;
    public float moveSpeed = 8f;
    public float sprintSpeed = 12f;
    public float roatationSpeed = 30f;
}
