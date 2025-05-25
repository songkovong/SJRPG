public abstract class BaseState
{
    protected Player player;

    public BaseState(Player player)
    {
        this.player = player;
    }


    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
