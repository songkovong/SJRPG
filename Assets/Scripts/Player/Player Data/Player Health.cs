using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerHealthData playerHealthData;
    Player player;

    private float currentHealth;
    private bool isGodmode;

    void Start()
    {
        player = GetComponent<Player>();
        currentHealth = playerHealthData.maxHealth;
        isGodmode = false;
    }

    public void TakeDamage(float getDamage)
    {
        if(isGodmode) return;
        if(player.GuardPressed) return;

        player.isHit = true;

        currentHealth -= getDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, playerHealthData.maxHealth);

        if(currentHealth <= 0) 
        {
            PlayerDie();
        }
        else 
        {
            StartCoroutine(GodmodeCoroutine());
        }

        Debug.Log("Player current Health = " + currentHealth);
    }

    private IEnumerator GodmodeCoroutine()
    {
        isGodmode = true;
        yield return new WaitForSeconds(playerHealthData.godmodeDuration);
        isGodmode = false;
    }

    public void PlayerDie()
    {
        Debug.Log("Die");
    }
}
