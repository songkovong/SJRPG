using UnityEngine;
public class DodgeState : BaseState
{
    private float timer;
    private float duration = 0.4f;

    public DodgeState(Player player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("Enter Dodge");
        timer = 0f;
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            player.ChangeState(
                player.InputDirection != Vector2.zero ? new MoveState(player) : new IdleState(player)
            );
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Dodge");
    }
}
