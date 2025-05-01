using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    Player player;

    // Health
    public float maxHealth { get; set; } = 100f;
    public float currentHealth { get; set; }
    public float godmodeDuration { get; private set; } = 2f;
    public bool isGodmode { get; set; } = false;

    // Damage
    public float attackDamage { get; private set; }
    public float finalDamage { get; set; }

    // Skill
    public List<PlayerSkillData> skills = new List<PlayerSkillData>();
    public float skillCooltimeTimer { get; set; }
    public bool canSkill { get; set; }
    string skillName;
    float skillCooltime;
    float skillDamage;

    // Weapon
    public List<WeaponDamageData> weapons = new List<WeaponDamageData>();
    string weaponName;
    public float weaponDamage { get; private set; }

    void Start()
    {
        player = GetComponent<Player>();

        currentHealth = maxHealth;
        isGodmode = false;

        attackDamage = 10f;

        FindSkills();
        skillCooltimeTimer = skillCooltime;
        canSkill = true;

        FindWeapons();
    }

    void Update()
    {
        if(!canSkill)
        {
            skillCooltimeTimer += Time.deltaTime;
            if(skillCooltimeTimer >= skillCooltime)
            {
                canSkill = true;
                skillCooltimeTimer = skillCooltime;
            }
        }
        Debug.Log("Can Skill = " + canSkill);
    }

    public void TakeDamage(float getDamage)
    {
        if(isGodmode) return;

        player.isHit = true;

        currentHealth -= getDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

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

        yield return new WaitForSeconds(godmodeDuration);

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
                this.skillCooltime = skill.cooltimeData;
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
    public float SkillCooltime => this.skillCooltime;
    public float SkillDamage => this.skillDamage;
}
