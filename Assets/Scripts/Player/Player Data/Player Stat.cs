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
        Debug.Log("Level = " + level);
    }

    void Initialize()
    {
        // Level
        if (PlayerPrefs.HasKey("Level")) level = PlayerPrefs.GetInt("Level");
        else level = 1;
        if(PlayerPrefs.HasKey("EXP")) exp = PlayerPrefs.GetFloat("EXP");
        else exp = 100;
        if (PlayerPrefs.HasKey("EXPCount")) expCount = PlayerPrefs.GetFloat("EXPCount");
        else expCount = 0;
        isLevelUp = false;

        // Health
        if (PlayerPrefs.HasKey("MAXHP")) maxHealth = PlayerPrefs.GetFloat("MAXHP");
        else maxHealth = 100;
        if(PlayerPrefs.HasKey("CurHP")) currentHealth = PlayerPrefs.GetFloat("CurHP");
        else currentHealth = maxHealth;

        isGodmode = false;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Stamina
        if (PlayerPrefs.HasKey("MAXStamina")) maxStamina = PlayerPrefs.GetFloat("MAXStamina");
        else maxStamina = 100;
        if(PlayerPrefs.HasKey("CurStamina")) currentStamina = PlayerPrefs.GetFloat("CurStamina");
        else currentStamina = maxStamina;

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        // Damage
        if(PlayerPrefs.HasKey("ATKDMG")) attackDamage = PlayerPrefs.GetFloat("ATKDMG");
        else attackDamage = 1f;

        // Skill
        if (PlayerPrefs.HasKey("SkillName")) player.skillName = PlayerPrefs.GetString("SkillName");
        else player.skillName = "Skill";
        FindSkills();
        skillCooltimeTimer = skillCooltime;
        canSkill = true;

        // Weapon
        if (PlayerPrefs.HasKey("WeaponName")) player.skillName = PlayerPrefs.GetString("WeaponName");
        else player.skillName = "Weapon";
        FindWeapons();
    }

    public void TakeDamage(float getDamage)
    {
        if (isGodmode) return;

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

        PlayerPrefs.SetString("SkillName", skillName);
        PlayerPrefs.SetString("WeaponName", weaponName);
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
        if (player.isHit)
        {
            player.PlayerHitColor.ChangeColor(player.PlayerHitColor.renderers, new Color(0.3f, 0, 0));
        }

        yield return new WaitForSeconds(godmodeDuration);

        isGodmode = false;
        player.PlayerHitColor.ReChangeColor(player.PlayerHitColor.renderers, player.PlayerHitColor.originalColors);
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
