using UnityEngine;

public class EnemyAnimator
{
    private readonly Animator animator;

    public EnemyAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void PlayHit()
    {
        animator.SetTrigger("GetHit");
    }

    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayChase(bool isChase)
    {
        animator.SetBool("isChase", isChase);
    }

    public void PlayCool(bool isCool)
    {
        animator.SetBool("isCool", isCool);
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
