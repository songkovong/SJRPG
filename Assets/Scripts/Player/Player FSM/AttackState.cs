using System.Collections;
using UnityEngine;

public class AttackState : BaseState
{
    private float animationDuration;
    private bool canCombo;
    private bool comboTrigger;
    private int comboCount;
    private float attackMoveSpeed = 0.3f;

    public AttackState(Player player, int comboCount = 1) : base(player)
    {
        this.comboCount = comboCount;
    }

    public override void Enter()
    {
        player.playerStat.finalDamage = player.playerStat.attackDamage + player.playerStat.weaponDamage;
        
        Debug.Log("Enter Attack");

        canCombo = false;
        comboTrigger = false;

        player.PlayerAnimator.PlayAttack(comboCount);

        var clip = player.PlayerAnimator.GetClipByName("Attack" + comboCount);
        animationDuration = clip != null ? clip.length : 0.5f;
        Debug.Log("clip length" + animationDuration);

        player.StartTrail();

        player.StartCoroutinePlayer(AllowComboAfterAnimation(animationDuration));
        player.StartCoroutinePlayer(EndAttackAfterTime(animationDuration));

        player.AttackHitboxOn();
    }

    public override void Update()
    {
        player.PlayerMove(attackMoveSpeed);
        player.PlayerAnimator.SetMove(
            player.InputDirection.magnitude * attackMoveSpeed * .75f, 
            player.localMovement.x, 
            player.localMovement.z
        );

        if (canCombo && player.AttackPressed)
        {
            comboTrigger = true;
        }
    }

    public override void Exit()
    {
        player.EndTrail();
        Debug.Log("Exit Attack");

        player.AttackHitboxOff();
    }

    private IEnumerator AllowComboAfterAnimation(float animationDuration)
    {
        yield return new WaitForSeconds(0.1f);

        canCombo = true;

        yield return new WaitForSeconds(animationDuration - 0.1f);

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
            player.ChangeState(new MoveState(player));
        }
    }
}