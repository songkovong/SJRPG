using UnityEngine;

public enum ENEMY_STATE
{
    Idle,
    Chase,
    Cool,
    Attack,
    Skill,
    Guard,
    Hit,
    Dead
}

public class EnemyState
{
    public ENEMY_STATE currentState { get; private set; }

    public bool ChangeState(ENEMY_STATE newState)
    {
        if(currentState == newState) return false;
        currentState = newState;
        return true;
    }
}
