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

    public void PlayDodge()
    {
        animator.SetTrigger("Dodge");
    }

    public void PlayGuard(bool isGuard)
    {
        animator.SetBool("isGuard", isGuard);
    }

    public AnimationClip GetCurrentClip(string name)
    {
        var clips = animator.GetCurrentAnimatorClipInfo(0);
        foreach (var clip in clips)
            if (clip.clip.name == name)
                return clip.clip;

        return null;
    }
}
