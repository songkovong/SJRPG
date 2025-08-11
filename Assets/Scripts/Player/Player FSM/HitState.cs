using UnityEngine;

public class HitState : BaseState
{
    public HitState(Player player) : base(player) { }

    float animationDuration;
    float timer;

    public override void Enter()
    {
        timer = 0f;

        SoundManager.Instance.Play2DSound("Hit Sound");

        Debug.Log("Enter Hit");
        player.PlayerAnimator.PlayHit();
        animationDuration = player.PlayerAnimator.GetClipByName("Hit").length;
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if(timer >= animationDuration)
        {
            player.ChangeState(new MoveState(player));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Hit");
        player.isHit = false;
    }
}
