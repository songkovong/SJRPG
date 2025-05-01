using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    Player player;

    // Health
    public PlayerHealthData playerHealthData;
    public float currentHealth { get; set; }
    public bool isGodmode { get; set; } = false;

    // Damage
    public PlayerDamageData playerDamageData;
    public float attackDamage { get; private set; }
    public float finalDamage { get; set; }

    // Skill
    public List<PlayerSkillData> skills = new List<PlayerSkillData>();
    public float skillCooltime { get; set; }
    public bool canSkill { get; set; }
    string skillName;
    float cooltime;
    float skillDamage;

    // Weapon
    public List<WeaponDamageData> weapons = new List<WeaponDamageData>();
    string weaponName;
    public float weaponDamage { get; private set; }
    public GameObject weaponHitbox;

    void Start()
    {
        player = GetComponent<Player>();

        currentHealth = playerHealthData.maxHealth;
        isGodmode = false;

        attackDamage = playerDamageData.attackDamageData;

        FindSkills();
        skillCooltime = cooltime;
        canSkill = true;

        FindWeapons();
    }

    void Update()
    {
        if(!canSkill)
        {
            skillCooltime += Time.deltaTime;
            if(skillCooltime >= cooltime)
            {
                canSkill = true;
                skillCooltime = cooltime;
            }
        }
        Debug.Log("Can Skill = " + canSkill);


        // HitValue(weaponHitbox.transform.position, new Vector3(0.2f, 1.2f, 0.1f), weaponHitbox.transform.rotation, "Enemy");
    }

    public void TakeDamage(float getDamage)
    {
        if(isGodmode) return;

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

    public void Heal(float getHealth)
    {
        currentHealth += getHealth;
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
        // player.ChangeState(new DieState(player));
    }

    void FindSkills()
    {
        foreach(PlayerSkillData skill in skills)
        {
            if(skill.nameData.Equals(player.skillName))
            {
                this.skillName = skill.nameData;
                this.cooltime = skill.cooltimeData;
                this.skillDamage = skill.damageData;
                return;
            }
        }
    }

    void FindWeapons()
    {
        foreach(WeaponDamageData weapon in weapons)
        {
            if(weapon.nameData.Equals(player.weaponName))
            {
                this.weaponName = weapon.nameData;
                this.weaponDamage = weapon.damageData;
                return;
            }
        }
    }


    public string SkillName => this.skillName;
    public float Cooltime => this.cooltime;
    public float SkillDamage => this.skillDamage;
}
