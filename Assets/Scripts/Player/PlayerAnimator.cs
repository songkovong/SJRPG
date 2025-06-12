using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerAnimator
{
    private readonly Animator animator;

    public PlayerAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void SetMove(float movement, float moveX, float moveY)
    {
        animator.SetFloat("movement", movement, 0.1f, Time.deltaTime);
        animator.SetFloat("moveX", moveX, 0.1f, Time.deltaTime);
        animator.SetFloat("moveY", moveY, 0.1f, Time.deltaTime);
    }

    public void PlayAttack(int comboStep)
    {
        animator.SetInteger("ComboStep", comboStep);
        animator.SetTrigger("Attack");
    }

    public void PlaySkill(string skillCode)
    {
        // animator.SetTrigger("Skill");
        // animator.Play("Skill " + skillCode);
        animator.CrossFade("Skill " + skillCode, 0.1f);
    }

    public void PlayHit()
    {
        animator.SetTrigger("Hit");
    }

    public void PlayDodge()
    {
        animator.SetTrigger("Dodge");
    }

    public void PlayGuard(bool isGuard)
    {
        animator.SetBool("isGuard", isGuard);
    }

    public void PlayLevelUp()
    {
        animator.SetTrigger("LevelUp");
    }

    public AnimationClip GetClipByName(string name)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
                return clip;
        }
        return null;
    }
}
