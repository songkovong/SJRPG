using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyState enemyState;

    protected virtual void Awake() 
    {
        enemyState = new EnemyState();
    }

    public void ChangeState(ENEMY_STATE newState)
    {
        // enemyState.ChangeState(newState);
        // OnStateChanged(newState);
        if(enemyState.ChangeState(newState))
        {
            OnStateChanged(newState);
        }
    }

    protected virtual void OnStateChanged(ENEMY_STATE newState)
    {
        Debug.Log($"{gameObject.name} changed to {newState} state");
    }

    public ENEMY_STATE GetCurrentState() => enemyState.currentState;
}
