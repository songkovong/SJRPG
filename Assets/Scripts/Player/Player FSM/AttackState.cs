using System.Collections;
using UnityEngine;

public class AttackState : BaseState
{
    private float animationDuration;
    private bool canCombo;
    private bool comboTrigger;
    private int comboCount;
    private float attackMoveSpeed = 0.3f;
    private float comboAttackMulti;

    public AttackState(Player player, int comboCount = 1) : base(player)
    {
        this.comboCount = comboCount;
    }

    public override void Enter()
    {
        if (comboCount == 1)
        {
            comboAttackMulti = 1f;
        }
        else if (comboCount == 2)
        {
            comboAttackMulti = 1.25f;
        }
        else if (comboCount == 3)
        {
            comboAttackMulti = 1.5f;
        }

        Debug.Log("Player Combo = " + player.playerStat.comboAttackSkill.playerCombo);

        player.playerStat.data.finalDamage = (int)(player.playerStat.AtkDmg() * comboAttackMulti);

        Debug.Log("Enter Attack");

        canCombo = false;
        comboTrigger = false;

        player.PlayerAnimator.PlayAttack(comboCount);

        var clip = player.PlayerAnimator.GetClipByName("Attack" + comboCount);
        animationDuration = clip != null ? clip.length : 0.5f;
        Debug.Log("clip length" + animationDuration);

        player.AttackTrail.StartTrail();
        // player.AttackHitbox.HitboxOn();
        if (comboCount == 1)
        {
            player.StartCoroutine(AttackHitBoxCoroutine(0.34f));
        }
        else if (comboCount == 2)
        {
            player.StartCoroutine(AttackHitBoxCoroutine(0.34f));
        }
        else if (comboCount == 3)
        {
            player.StartCoroutine(AttackHitBoxCoroutine(0.37f));
        }

        player.StartCoroutinePlayer(AllowComboAfterAnimation(animationDuration));
        player.StartCoroutinePlayer(EndAttackAfterTime(animationDuration));
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
        player.AttackTrail.EndTrail();
        // player.AttackHitbox.HitboxOff();

        Debug.Log("Exit Attack");
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
        if (comboTrigger && comboCount < player.playerStat.comboAttackSkill.playerCombo)
        {
            player.ChangeState(new AttackState(player, comboCount + 1));
        }
        else
        {
            player.ChangeState(new MoveState(player));
        }
    }
    
    public IEnumerator AttackHitBoxCoroutine(float delayTime)
    {
        player.AttackHitbox.HitboxOn();

        yield return new WaitForSeconds(delayTime);

        player.AttackHitbox.HitboxOff();
    }
}