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

    // Stamina 
    public float maxStamina { get; set; } = 100f;
    public float currentStamina { get; set; }

    // Damage
    public float attackDamage { get; private set; }
    public float finalDamage { get; set; }

    // Skill
    public List<PlayerSkillData> skills = new List<PlayerSkillData>();
    public float skillCooltimeTimer { get; set; }
    public bool canSkill { get; set; }
    string skillName;
    public float skillCooltime { get; private set; }
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        currentStamina = maxStamina;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        attackDamage = 1f;

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
        
        GameObject dmgtext = Instantiate(player.damageText);
        dmgtext.transform.position = player.damagePos.position;
        dmgtext.GetComponent<DamageText>().damage = getDamage;

        if (currentHealth <= 0)
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

    public void AtkDmgUp(float dmgUp)
    {
        attackDamage += dmgUp;
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
