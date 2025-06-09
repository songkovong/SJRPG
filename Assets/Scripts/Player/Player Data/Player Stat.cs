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

    // Magic
    public float maxMagic { get; set; } = 100f;
    public float currentMagic { get; set; }
    public bool canMagic;
    public float magicRecoveryRate { get; private set; }

    // Speed
    public float moveSpeed { get; set; } = 6f;
    public float sprintSpeed { get; set; } = 10f;
    public float rotationSpeed { get; set; } = 30f;

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
    float skillMagic;

    // Weapon
    public List<WeaponDamageData> weapons = new List<WeaponDamageData>();
    int weaponCode;
    public float weaponDamage { get; private set; }

    // Stat Point
    public int statPoint { get; private set; }
    public int strength { get; private set; }
    public int agility { get; private set; }
    public int magic { get; private set; }


    void Start()
    {
        player = GetComponent<Player>();

        // DeleteData();

        Initialize();
    }

    void Update()
    {
        SkillCooltimeRecovery();
        MagicRecovery();

        if (currentMagic >= skillMagic)
        {
            canMagic = true;
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

        // Magic
        maxMagic = PlayerPrefs.HasKey("MAXMagic") ? PlayerPrefs.GetFloat("MAXMagic") : 100;
        currentMagic = PlayerPrefs.HasKey("CurMagic") ? PlayerPrefs.GetFloat("CurMagic") : maxMagic;
        currentMagic = Mathf.Clamp(currentMagic, 0, maxMagic);
        magicRecoveryRate = PlayerPrefs.HasKey("MagicRecovery") ? PlayerPrefs.GetFloat("MagicRecovery") : 0.1f;
        canMagic = true;

        // Speed
        moveSpeed = PlayerPrefs.HasKey("Speed") ? PlayerPrefs.GetFloat("Speed") : 6f;
        sprintSpeed = PlayerPrefs.HasKey("SprintSpeed") ? PlayerPrefs.GetFloat("SprintSpeed") : 10f;
        rotationSpeed = PlayerPrefs.HasKey("RotationSpeed") ? PlayerPrefs.GetFloat("RotationSpeed") : 30f;

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

        // Stat Point
        statPoint = PlayerPrefs.HasKey("StatPoint") ? PlayerPrefs.GetInt("StatPoint") : 0;
        strength = PlayerPrefs.HasKey("StrengthPoint") ? PlayerPrefs.GetInt("StrengthPoint") : 1;
        agility = PlayerPrefs.HasKey("AgilityPoint") ? PlayerPrefs.GetInt("AgilityPoint") : 1;
        magic = PlayerPrefs.HasKey("MagicPoint") ? PlayerPrefs.GetInt("MagicPoint") : 1;
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
            exp *= 1.4f;
            expCount = 0;

            statPoint += 3;

            SaveData();
        }
    }

    public void StrengthUp()
    {
        if (statPoint >= 1)
        {
            maxHealth *= 1.05f;
            attackDamage *= 1.05f;
            statPoint -= 1;
            strength++;
            SaveData();
        }
    }

    public void AgilityUp()
    {
        if (statPoint >= 1)
        {
            moveSpeed += 0.15f;
            sprintSpeed += 0.3f;
            statPoint -= 1;
            agility++;
            SaveData();
        }
    }

    public void MagicUp()
    {
        if (statPoint >= 1)
        {
            maxMagic *= 1.05f;
            magicRecoveryRate += 0.02f;
            statPoint -= 1;
            magic++;
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

        PlayerPrefs.SetFloat("MAXMagic", maxMagic);
        PlayerPrefs.SetFloat("CurMagic", currentMagic);
        PlayerPrefs.SetFloat("MagicRecovery", magicRecoveryRate);

        PlayerPrefs.SetFloat("Speed", moveSpeed);
        PlayerPrefs.SetFloat("SprintSpeed", sprintSpeed);

        PlayerPrefs.SetFloat("ATKDMG", attackDamage);

        PlayerPrefs.SetInt("SkillCode", skillCode);
        PlayerPrefs.SetInt("WeaponCode", weaponCode);

        PlayerPrefs.SetInt("StatPoint", statPoint);
        PlayerPrefs.SetInt("StrengthPoint", strength);
        PlayerPrefs.SetInt("AgilityPoint", agility);
        PlayerPrefs.SetInt("MagicPoint", magic);
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
        return (int)(Mathf.Floor((skillDamage * RandomAtkDmg() + weaponDamage) * 10f));
    }

    void MagicRecovery()
    {
        if (maxMagic >= currentMagic)
        {
            currentMagic += Time.deltaTime * magicRecoveryRate;
        }
    }

    void SkillCooltimeRecovery()
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
        foreach (PlayerSkillData skill in skills)
        {
            if (skill.skillCode.Equals(player.skillCode))
            {
                this.skillCode = skill.skillCode;
                this.skillCooltime = skill.cooltimeData;
                this.skillDamage = skill.damageData;
                this.skillMagic = skill.magicData;
                return;
            }
        }
    }

    void FindWeapons()
    {
        foreach (WeaponDamageData weapon in weapons)
        {
            if (weapon.weaponCode.Equals(player.weaponCode))
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
    public float SkillMagic => this.skillMagic;
}
