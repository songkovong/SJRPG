using UnityEngine;
 
public class BossAnimator
{
    private readonly Animator animator;

    public BossAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void PlayHit()
    {
        animator.SetTrigger("GetHit");
    }

    public void PlayRandomAttack(int random)
    {
        string randStr = Random.Range(1, random).ToString();
        animator.SetTrigger("Attack " + randStr);
    }

    public void PlayChase(bool isChase)
    {
        animator.SetBool("isChase", isChase);
    }

    public void PlayCool(bool isCool)
    {
        animator.SetBool("isCool", isCool);
    }

    public void PlayDead(bool isDead)
    {
        animator.SetBool("isDead", isDead);
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
