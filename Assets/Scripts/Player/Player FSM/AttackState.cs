using System.Collections;
using UnityEngine;

// public class AttackState : BaseState
// {
//     private float timer;
//     private float attackDuration;

//     public AttackState(Player player) : base(player) { }

//     public override void Enter()
//     {
//         Debug.Log("Enter Attack");
//         player.PlayerAnimator.PlayAttack();

//         // Get Animation Length
//         AnimatorStateInfo stateInfo = player.Animator.GetCurrentAnimatorStateInfo(0);
//         attackDuration = stateInfo.length;

//         timer = 0f;
//     }

//     public override void Update()
//     {
//         timer += Time.deltaTime;
//         if (timer >= attackDuration)
//         {
//             player.ChangeState(
//                 player.InputDirection != Vector2.zero ?
//                 new MoveState(player) : new IdleState(player)
//             );
//         }
//     }

//     public override void Exit()
//     {
//         Debug.Log("Exit Attack");
//     }
// }

public class AttackState : BaseState
{
    private float animationDuration;
    private bool canCombo;
    private bool comboTrigger;
    private int comboCount;

    public AttackState(Player player, int comboCount = 1) : base(player)
    {
        this.comboCount = comboCount;
    }

    public override void Enter()
    {
        Debug.Log("Enter Attack");

        canCombo = false;
        comboTrigger = false;

        player.PlayerAnimator.PlayAttack(comboCount);

        // var clip = player.PlayerAnimator.GetCurrentClip("Attack" + comboCount);
        var clip = player.PlayerAnimator.GetClipByName("Attack" + comboCount);
        animationDuration = clip != null ? clip.length : 0.5f;
        Debug.Log("clip length" + animationDuration);

        player.StartCoroutinePlayer(AllowComboAfterAnimation(animationDuration));
        player.StartCoroutinePlayer(EndAttackAfterTime(animationDuration));
    }

    public override void Update()
    {
        if (canCombo && player.AttackPressed)
        {
            comboTrigger = true;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Attack");
    }

    private IEnumerator AllowComboAfterAnimation(float animationDuration)
    {
        yield return new WaitForSeconds(0.3f);

        canCombo = true;

        yield return new WaitForSeconds(animationDuration - 0.3f);

        canCombo = false;
    }

    private IEnumerator EndAttackAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (comboTrigger && comboCount < 3)
        {
            player.ChangeState(new AttackState(player, comboCount + 1));
        }
        else
        {
            // player.ChangeState(
            //     player.InputDirection != Vector2.zero ? new MoveState(player) : new IdleState(player)
            // );

            player.ChangeState(new MoveState(player));
        }
    }
}