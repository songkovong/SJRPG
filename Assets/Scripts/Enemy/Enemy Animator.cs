using UnityEngine;

public class EnemyAnimator : MonoBehaviour
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
