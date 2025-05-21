using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    Player player;

    // Level
    public int level { get; set; } = 1;
    public float exp { get; set; } = 100;
    public float expCount { get; set; } = 0;
    public bool isLevelUp { get; set; }

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
    public int finalDamage { get; set; }

    // Skill
    public List<PlayerSkillData> skills = new List<PlayerSkillData>();
    public float skillCooltimeTimer { get; set; }
    public bool canSkill { get; set; }
    int skillCode;
    public float skillCooltime { get; private set; }
    float skillDamage;

    // Weapon
    public List<WeaponDamageData> weapons = new List<WeaponDamageData>();
    int weaponCode;
    public float weaponDamage { get; private set; }

    void Start()
    {
        player = GetComponent<Player>();

        // DeleteData();

        Initialize();
    }

    void Update()
    {
        if (!canSkill)
        {
            skillCooltimeTimer += Time.deltaTime;
            if (skillCooltimeTimer >= skillCooltime)
            {
                canSkill = true;
                skillCooltimeTimer = skillCooltime;
            }
        }

        LvUp();
    }

    void Initialize()
    {
        // Level
        level = PlayerPrefs.HasKey("Level") ? PlayerPrefs.GetInt("Level") : 1;
        exp = PlayerPrefs.HasKey("EXP") ? PlayerPrefs.GetFloat("EXP") : 100;
        expCount = PlayerPrefs.HasKey("EXPCount") ? PlayerPrefs.GetFloat("EXPCount") : 0;

        isLevelUp = false;

        // Health
        maxHealth = PlayerPrefs.HasKey("MAXHP") ? PlayerPrefs.GetFloat("MAXHP") : 100;
        currentHealth = PlayerPrefs.HasKey("CurHP") ? PlayerPrefs.GetFloat("CurHP") : maxHealth;
        isGodmode = false;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Stamina
        maxStamina = PlayerPrefs.HasKey("MAXStamina") ? PlayerPrefs.GetFloat("MAXStamina") : 100;
        currentStamina = PlayerPrefs.HasKey("CurStamina") ? PlayerPrefs.GetFloat("CurStamina") : maxStamina;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        // Damage
        attackDamage = PlayerPrefs.HasKey("ATKDMG") ? PlayerPrefs.GetFloat("ATKDMG") : 1;

        // Skill
        player.skillCode = PlayerPrefs.HasKey("SkillCode") ? PlayerPrefs.GetInt("SkillCode") : 1;
        FindSkills();
        skillCooltimeTimer = skillCooltime;
        canSkill = true;

        // Weapon

        player.weaponCode = PlayerPrefs.HasKey("WeaponCode") ? PlayerPrefs.GetInt("WeaponCode") : 1;
        FindWeapons();
    }

    public void TakeDamage(float getDamage)
    {
        if(isGodmode) return;

        player.isHit = true;

        currentHealth -= getDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Damage Text
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

        StartCoroutine(HitColorCoroutine());

        Debug.Log("Player current Health = " + currentHealth);
    }

    public void Heal(float getHealth)
    {
        currentHealth += getHealth;
    }

    public void LvUp()
    {
        if (expCount >= exp)
        {
            isLevelUp = true;
            
            level++;
            exp *= 1.3f;
            expCount = 0;

            maxHealth *= 1.1f;
            currentHealth = maxHealth;

            maxStamina *= 1.1f;
            currentStamina = maxStamina;

            attackDamage *= 1.2f;

            SaveData();
        }
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetFloat("EXP", exp);
        PlayerPrefs.SetFloat("EXPCount", expCount);

        PlayerPrefs.SetFloat("MAXHP", maxHealth);
        PlayerPrefs.SetFloat("CurHP", currentHealth);

        PlayerPrefs.SetFloat("MAXStamina", maxStamina);
        PlayerPrefs.SetFloat("CurStamina", currentStamina);

        PlayerPrefs.SetFloat("ATKDMG", attackDamage);

        PlayerPrefs.SetInt("SkillCode", skillCode);
        PlayerPrefs.SetInt("WeaponCode", weaponCode);
    }

    public float RandomAtkDmg()
    {
        return Random.Range(attackDamage * 0.1f, attackDamage);
    }

    public int AtkDmg()
    {
        return (int)(Mathf.Floor((RandomAtkDmg() + weaponDamage) * 10f));
    }

    public int SkillDmg()
    {
        return (int)(Mathf.Floor((SkillDamage * RandomAtkDmg() + weaponDamage) * 10f));
    }


    private IEnumerator GodmodeCoroutine()
    {
        isGodmode = true;

        yield return new WaitForSeconds(godmodeDuration);

        isGodmode = false;
    }

    private IEnumerator HitColorCoroutine()
    {
        player.PlayerHitColor.ChangeColor(player.PlayerHitColor.renderers, Color.grey);

        yield return new WaitForSeconds(godmodeDuration);

        player.PlayerHitColor.ReChangeColor(player.PlayerHitColor.renderers, player.PlayerHitColor.originalColors);
    }

    public void PlayerDie()
    {
        Debug.Log("Die");
    }

    void FindSkills()
    {
        foreach(PlayerSkillData skill in skills)
        {
            if(skill.skillCode.Equals(player.skillCode))
            {
                this.skillCode = skill.skillCode;
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
            if(weapon.weaponCode.Equals(player.weaponCode))
            {
                this.weaponCode = weapon.weaponCode;
                this.weaponDamage = weapon.damageData;
                return;
            }
        }
    }


    public int SkillCode => this.skillCode;
    public float SkillCooltime => this.skillCooltime;
    public float SkillDamage => this.skillDamage;
}
