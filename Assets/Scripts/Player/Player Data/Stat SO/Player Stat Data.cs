using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatData", menuName = "Game/PlayerStatData")]
public class PlayerStatData : ScriptableObject
{
    // Level
    public int level = 1;
    public float exp = 100;
    public float expCount = 0;
    public bool isLevelUp;

    // Health
    public float maxHealth = 100f;
    public float currentHealth;
    public float godmodeDuration = 2f;
    public bool isGodmode;

    // Magic
    public float maxMagic = 100f;
    public float currentMagic;
    public bool canMagic = true;
    public float magicRecoveryRate = 0.1f;

    // Speed
    public float moveSpeed = 6f;
    public float sprintSpeed = 10f;
    public float rotationSpeed = 30f;
    public float attackSpeed = 0.8f;

    // Depend
    public float dependRate = 0;

    // Damage
    public float attackDamage = 1f;
    public float buffDamage;
    public int finalDamage;
    public int minFinalDamage;
    public int maxFinalDamage;

    // Weapon
    public int weaponCode;

    // Stat Point
    public int statPoint;
    public int strength = 1;
    public int agility = 1;
    public int magic = 1;
    public int depend = 1;

    // Skill Stat Point
    public int skillStatPoint;

    public void Save()
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
        PlayerPrefs.SetFloat("AttackSpeed", attackSpeed);

        PlayerPrefs.SetFloat("DependRate", dependRate);

        PlayerPrefs.SetFloat("ATKDMG", attackDamage);

        PlayerPrefs.SetInt("WeaponCode", weaponCode);

        PlayerPrefs.SetInt("StatPoint", statPoint);
        PlayerPrefs.SetInt("StrengthPoint", strength);
        PlayerPrefs.SetInt("AgilityPoint", agility);
        PlayerPrefs.SetInt("MagicPoint", magic);
        PlayerPrefs.SetInt("DependPoint", depend);

        PlayerPrefs.SetInt("SkillStatPoint", skillStatPoint);

        PlayerPrefs.Save();
    }

    public void Load()
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
        attackSpeed = PlayerPrefs.HasKey("AttackSpeed") ? PlayerPrefs.GetFloat ("AttackSpeed") : 0.8f;

        // Depend
        dependRate = PlayerPrefs.HasKey("DependRate") ? PlayerPrefs.GetFloat("DependRate") : 0;

        // Damage
        attackDamage = PlayerPrefs.HasKey("ATKDMG") ? PlayerPrefs.GetFloat("ATKDMG") : 1;

        // Weapon
        weaponCode = PlayerPrefs.HasKey("WeaponCode") ? PlayerPrefs.GetInt("WeaponCode") : 1;
        // FindWeapons();

        // Stat Point
        statPoint = PlayerPrefs.HasKey("StatPoint") ? PlayerPrefs.GetInt("StatPoint") : 10000;
        strength = PlayerPrefs.HasKey("StrengthPoint") ? PlayerPrefs.GetInt("StrengthPoint") : 1;
        agility = PlayerPrefs.HasKey("AgilityPoint") ? PlayerPrefs.GetInt("AgilityPoint") : 1;
        magic = PlayerPrefs.HasKey("MagicPoint") ? PlayerPrefs.GetInt("MagicPoint") : 1;
        depend = PlayerPrefs.HasKey("DependPoint") ? PlayerPrefs.GetInt("DependPoint") : 1;

        skillStatPoint = PlayerPrefs.HasKey("SkillStatPoint") ? PlayerPrefs.GetInt("SkillStatPoint") : 10000;
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
