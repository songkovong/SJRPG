// using UnityEngine;

// public class IdleState : BaseState
// {
//     public IdleState(Player player) : base(player) { }

//     public override void Enter()
//     {
//         Debug.Log("Enter Idle");
//     }

//     public override void Update()
//     {
//         player.PlayerAnimator.SetMove(0);
//         if (player.InputDirection.magnitude > 0f)
//         {
//             player.ChangeState(new MoveState(player));
//             return;
//         }

//         if (player.AttackPressed)
//         {
//             player.ChangeState(new AttackState(player));
//             return;
//         }

//         if (player.SkillPressed)
//         {
//             player.ChangeState(new SkillState(player));
//             return;
//         }

//         if (player.DodgePressed)
//         {
//             player.ChangeState(new DodgeState(player));
//             return;
//         }

//         if (player.GuardPressed)
//         {
//             player.ChangeState(new GuardState(player));
//             return;
//         }
//     }

//     public override void Exit()
//     {
//         Debug.Log("Exit Idle");
//     }
// }
