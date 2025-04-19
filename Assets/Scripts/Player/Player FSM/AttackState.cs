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
    private bool comboAllowed;
    private bool comboQueued;
    private int comboStep;

    public AttackState(Player player, int comboStep = 1) : base(player)
    {
        this.comboStep = comboStep;
    }

    public override void Enter()
    {
        comboAllowed = false;
        comboQueued = false;

        // 애니메이션 실행
        player.PlayerAnimator.PlayAttack(comboStep);

        // 애니메이션 길이 가져오기
        var clip = player.PlayerAnimator.GetCurrentClip("Attack" + comboStep);
        animationDuration = clip != null ? clip.length : 0.5f;

        // combo 허용 타이밍 예약 (70% 시점에 허용)
        // player.StartCoroutine(AllowComboAfterTime(animationDuration * 0.7f));
        player.StartCoroutine(AllowComboAfterAnimation(animationDuration));
        player.StartCoroutine(EndAttackAfterTime(animationDuration));
    }

    public override void Update()
    {
        if (comboAllowed && player.AttackPressed)
        {
            comboQueued = true;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Attack");
    }

    private IEnumerator AllowComboAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        comboAllowed = true;
    }

    private IEnumerator AllowComboAfterAnimation(float animationDuration)
    {
        // 애니메이션 끝날 때까지 기다림
        yield return new WaitForSeconds(animationDuration - 0.3f);

        // 그때부터 콤보 입력 허용
        comboAllowed = true;

        // 콤보 입력을 받을 수 있는 시간 (예: 0.3초) 설정 후 자동 종료
        yield return new WaitForSeconds(0.5f); // or longer
        comboAllowed = false;
    }

    private IEnumerator EndAttackAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (comboQueued && comboStep < 3)
        {
            player.ChangeState(new AttackState(player, comboStep + 1));
        }
        else
        {
            player.ChangeState(
                player.InputDirection != Vector2.zero ? new MoveState(player) : new IdleState(player)
            );
        }
    }
}
