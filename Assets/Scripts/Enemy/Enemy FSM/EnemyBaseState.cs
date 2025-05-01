public abstract class EnemyBaseState
{
    protected Enemy enemy;

    public EnemyBaseState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
