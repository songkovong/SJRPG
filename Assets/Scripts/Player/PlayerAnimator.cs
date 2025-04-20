using UnityEngine;

public class PlayerAnimator
{
    private readonly Animator animator;

    public PlayerAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void SetMove(float movement)
    {
        animator.SetFloat("movement", movement, 0.1f, Time.deltaTime);
    }

    public void PlayAttack(int comboStep)
    {
        animator.SetInteger("ComboStep", comboStep);
        animator.SetTrigger("Attack");
    }

    public void PlaySkill()
    {
        animator.SetTrigger("Skill");
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
