public abstract class BossBaseState
{
    protected BossEnemy bossEnemy;

    public BossBaseState(BossEnemy bossEnemy)
    {
        this.bossEnemy = bossEnemy;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
