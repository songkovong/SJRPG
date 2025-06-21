using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    Player player;

    // // Level
    // public int level { get; set; } = 1;
    // public float exp { get; set; } = 100;
    // public float expCount { get; set; } = 0;
    // public bool isLevelUp { get; set; }

    // // Health
    // public float maxHealth { get; set; } = 100f;
    // public float currentHealth { get; set; }
    // public float godmodeDuration { get; private set; } = 2f;
    // public bool isGodmode { get; set; } = false;

    // // Magic
    // public float maxMagic { get; set; } = 100f;
    // public float currentMagic { get; set; }
    // public bool canMagic { get; private set; }
    // public float magicRecoveryRate { get; private set; }

    // // Speed
    // public float moveSpeed { get; set; } = 6f;
    // public float sprintSpeed { get; set; } = 10f;
    // public float rotationSpeed { get; set; } = 30f;

    // // Damage
    // public float attackDamage { get; private set; }
    // public float buffDamage { get; private set; }
    // public int finalDamage { get; set; }

    // Skill
    public SpaceSkill spaceSkill { get; set; }
    public CSkill cSkill { get; set; }
    public RSkill rSkill { get; set; }

    // Weapon
    public List<WeaponDamageData> weapons = new List<WeaponDamageData>();
    int weaponCode;
    public float weaponDamage { get; private set; }

    // // Stat Point
    // public int statPoint { get; private set; }
    // public int strength { get; private set; }
    // public int agility { get; private set; }
    // public int magic { get; private set; }

    public PlayerStatData data;

    void Start()
    {
        player = GetComponent<Player>();

        spaceSkill = GetComponent<SpaceSkill>();
        cSkill = GetComponent<CSkill>();
        rSkill = GetComponent<RSkill>();

        data.Load();

        //DeleteAllData();

        // Load();
    }

    void Update()
    {
        MagicRecovery();
        LvUp();
    }

    public void TakeDamage(float getDamage)
    {
        if (data.isGodmode) return;

        player.isHit = true;

        data.currentHealth -= getDamage;
        data.currentHealth = Mathf.Clamp(data.currentHealth, 0, data.maxHealth);

        // Damage Text
        GameObject dmgtext = Instantiate(player.damageText);
        dmgtext.transform.position = player.damagePos.position;
        dmgtext.GetComponent<DamageText>().damage = getDamage;

        if (data.currentHealth <= 0)
        {
            PlayerDie();
        }
        else
        {
            StartCoroutine(GodmodeCoroutine());
        }

        StartCoroutine(HitColorCoroutine());

        Debug.Log("Player current Health = " + data.currentHealth);
    }

    public void Heal(float getHealth)
    {
        data.currentHealth += getHealth;
    }

    public void LvUp()
    {
        if (data.expCount >= data.exp)
        {
            data.isLevelUp = true;

            data.level++;
            data.exp *= 1.4f;
            data.expCount = 0;

            data.statPoint += 3;

            data.Save();
        }
    }

    public void StrengthUp()
    {
        if (data.statPoint >= 1)
        {
            data.maxHealth *= 1.05f;
            data.attackDamage *= 1.05f;
            data.statPoint -= 1;
            data.strength++;
            data.Save();
        }
    }

    public void AgilityUp()
    {
        if (data.statPoint >= 1)
        {
            data.moveSpeed += 0.15f;
            data.sprintSpeed += 0.3f;
            data.statPoint -= 1;
            data.agility++;
            data.Save();
        }
    }

    public void MagicUp()
    {
        if (data.statPoint >= 1)
        {
            data.maxMagic *= 1.05f;
            data.magicRecoveryRate += 0.02f;
            data.statPoint -= 1;
            data.magic++;
            data.Save();
        }
    }

    public float RandomAtkDmg()
    {
        return Random.Range(data.attackDamage * 0.1f, data.attackDamage);
    }

    public int AtkDmg()
    {
        return (int)(Mathf.Floor((RandomAtkDmg() + weaponDamage) * 10f));
    }

    public int SkillDmg(float skilldmg)
    {
        return (int)(Mathf.Floor((skilldmg * RandomAtkDmg() + weaponDamage) * 10f));
    }

    void MagicRecovery()
    {
        if (data.maxMagic >= data.currentMagic)
        {
            data.currentMagic += Time.deltaTime * data.magicRecoveryRate;
        }
    }

    private IEnumerator GodmodeCoroutine()
    {
        data.isGodmode = true;

        yield return new WaitForSeconds(data.godmodeDuration);

        data.isGodmode = false;
    }

    private IEnumerator HitColorCoroutine()
    {
        player.PlayerHitColor.ChangeColor(player.PlayerHitColor.renderers, Color.grey);

        yield return new WaitForSeconds(data.godmodeDuration);

        player.PlayerHitColor.ReChangeColor(player.PlayerHitColor.renderers, player.PlayerHitColor.originalColors);
    }

    public IEnumerator BuffDamage()
    {
        data.attackDamage *= 2;
        yield return new WaitForSeconds(cSkill.duration);
        data.attackDamage /= 2;
    }

    public void PlayerDie()
    {
        Debug.Log("Die");
    }

    void FindWeapons()
    {
        foreach (WeaponDamageData weapon in weapons)
        {
            if (weapon.weaponCode.Equals(data.weaponCode))
            {
                this.weaponCode = weapon.weaponCode;
                this.weaponDamage = weapon.damageData;
                return;
            }
        }
    }

    #region Data Save and Load
    // void Load()
    // {
    //     // Level
    //     level = PlayerPrefs.HasKey("Level") ? PlayerPrefs.GetInt("Level") : 1;
    //     exp = PlayerPrefs.HasKey("EXP") ? PlayerPrefs.GetFloat("EXP") : 100;
    //     expCount = PlayerPrefs.HasKey("EXPCount") ? PlayerPrefs.GetFloat("EXPCount") : 0;

    //     isLevelUp = false;

    //     // Health
    //     maxHealth = PlayerPrefs.HasKey("MAXHP") ? PlayerPrefs.GetFloat("MAXHP") : 100;
    //     currentHealth = PlayerPrefs.HasKey("CurHP") ? PlayerPrefs.GetFloat("CurHP") : maxHealth;
    //     isGodmode = false;
    //     currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    //     // Magic
    //     maxMagic = PlayerPrefs.HasKey("MAXMagic") ? PlayerPrefs.GetFloat("MAXMagic") : 100;
    //     currentMagic = PlayerPrefs.HasKey("CurMagic") ? PlayerPrefs.GetFloat("CurMagic") : maxMagic;
    //     currentMagic = Mathf.Clamp(currentMagic, 0, maxMagic);
    //     magicRecoveryRate = PlayerPrefs.HasKey("MagicRecovery") ? PlayerPrefs.GetFloat("MagicRecovery") : 0.1f;
    //     canMagic = true;

    //     // Speed
    //     moveSpeed = PlayerPrefs.HasKey("Speed") ? PlayerPrefs.GetFloat("Speed") : 6f;
    //     sprintSpeed = PlayerPrefs.HasKey("SprintSpeed") ? PlayerPrefs.GetFloat("SprintSpeed") : 10f;
    //     rotationSpeed = PlayerPrefs.HasKey("RotationSpeed") ? PlayerPrefs.GetFloat("RotationSpeed") : 30f;

    //     // Damage
    //     attackDamage = PlayerPrefs.HasKey("ATKDMG") ? PlayerPrefs.GetFloat("ATKDMG") : 1;

    //     // Weapon
    //     player.weaponCode = PlayerPrefs.HasKey("WeaponCode") ? PlayerPrefs.GetInt("WeaponCode") : 1;
    //     FindWeapons();

    //     // Stat Point
    //     statPoint = PlayerPrefs.HasKey("StatPoint") ? PlayerPrefs.GetInt("StatPoint") : 0;
    //     strength = PlayerPrefs.HasKey("StrengthPoint") ? PlayerPrefs.GetInt("StrengthPoint") : 1;
    //     agility = PlayerPrefs.HasKey("AgilityPoint") ? PlayerPrefs.GetInt("AgilityPoint") : 1;
    //     magic = PlayerPrefs.HasKey("MagicPoint") ? PlayerPrefs.GetInt("MagicPoint") : 1;
    // }

    // public void SaveData()
    // {
    //     PlayerPrefs.SetInt("Level", level);
    //     PlayerPrefs.SetFloat("EXP", exp);
    //     PlayerPrefs.SetFloat("EXPCount", expCount);

    //     PlayerPrefs.SetFloat("MAXHP", maxHealth);
    //     PlayerPrefs.SetFloat("CurHP", currentHealth);

    //     PlayerPrefs.SetFloat("MAXMagic", maxMagic);
    //     PlayerPrefs.SetFloat("CurMagic", currentMagic);
    //     PlayerPrefs.SetFloat("MagicRecovery", magicRecoveryRate);

    //     PlayerPrefs.SetFloat("Speed", moveSpeed);
    //     PlayerPrefs.SetFloat("SprintSpeed", sprintSpeed);

    //     PlayerPrefs.SetFloat("ATKDMG", attackDamage);

    //     PlayerPrefs.SetInt("WeaponCode", weaponCode);

    //     PlayerPrefs.SetInt("StatPoint", statPoint);
    //     PlayerPrefs.SetInt("StrengthPoint", strength);
    //     PlayerPrefs.SetInt("AgilityPoint", agility);
    //     PlayerPrefs.SetInt("MagicPoint", magic);

    //     PlayerPrefs.Save();
    // }

    // public void DeleteAllData()
    // {
    //     PlayerPrefs.DeleteAll();
    // }

    // public IEnumerator AutoSaveRoutine()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(30f);
    //         SaveData();
    //         Debug.Log("자동 저장됨");
    //     }
    // }
#endregion
}
