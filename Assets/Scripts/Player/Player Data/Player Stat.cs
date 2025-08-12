using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    Player player;

    // Skill
    public SpaceSkill spaceSkill { get; set; }
    public CSkill cSkill { get; set; }
    public RSkill rSkill { get; set; }
    public AttackMasterySkill attackMastery { get; set; }
    public GuardSkill guardSkill { get; set; }
    public ComboAttackSkill comboAttackSkill { get; set; }

    // Weapon
    public float weaponDamage { get; set; } = 1f;
    public float weaponSpeed { get; set; } = 1f;

    public PlayerStatData data;


    #region Life Cycle

    void Awake()
    {
        // DeleteAllData();
    }

    void Start()
    {
        player = GetComponent<Player>();

        spaceSkill = GetComponent<SpaceSkill>();
        cSkill = GetComponent<CSkill>();
        rSkill = GetComponent<RSkill>();
        attackMastery = GetComponent<AttackMasterySkill>();
        guardSkill = GetComponent<GuardSkill>();
        comboAttackSkill = GetComponent<ComboAttackSkill>();

        // LoadAllData();
        StartCoroutine(LoadDataCoroutine());
    }

    void Update()
    {
        MagicRecovery();
        LvUp();
    }

    #endregion

    #region Take Damage and Heal (Player Health Methods)

    public void TakeDamage(float getDamage)
    {
        if (data.isGodmode) return;
        if (player.isGuard) return;

        getDamage = (int)(getDamage * (1 - Random.Range(data.dependRate * 0.5f, data.dependRate)));

        if (getDamage == 0)
        {
            GameObject missText = Instantiate(player.damageText);
            missText.transform.position = player.damagePos.position;
            missText.GetComponent<DamageText>().MissText();
            return;
        }

        player.isHit = true;

        data.currentHealth -= getDamage;
        data.currentHealth = Mathf.Clamp(data.currentHealth, 0, data.maxHealth);

        // Damage Text
        GameObject dmgtext = Instantiate(player.damageText);
        dmgtext.transform.position = player.damagePos.position;
        // dmgtext.GetComponent<DamageText>().damage = getDamage;
        dmgtext.GetComponent<DamageText>().DmgText(getDamage);

        if (data.currentHealth <= 0)
        {
            PlayerDie();
        }
        else
        {
            StartCoroutine(GodmodeCoroutine());
        }

        StartCoroutine(HitColorCoroutine());
        StartCoroutine(HitCoroutine());

        Debug.Log("Player current Health = " + data.currentHealth);
    }

    public void Heal(float getHealth)
    {
        data.currentHealth = Mathf.Min(data.currentHealth + getHealth, data.maxHealth);
    }

    public void MPHeal(float getMagic)
    {
        data.currentMagic = Mathf.Min(data.currentMagic + getMagic, data.maxMagic);
    }

    #endregion

    #region Level Up and Stat Up

    public void LvUp()
    {
        if (data.expCount >= data.exp)
        {
            data.isLevelUp = true;

            data.level++;
            data.exp *= 1.4f;
            data.expCount = 0;

            data.statPoint += 3;
            data.skillStatPoint++;

            data.currentHealth = data.maxHealth;
            data.currentMagic = data.maxMagic;

            SaveAllData();
        }
    }

    public void StrengthUp()
    {
        if (data.statPoint >= 1)
        {
            data.maxHealth *= 1.05f;
            data.attackDamage += 1.1f;
            data.statPoint -= 1;
            data.strength++;
            SaveAllData();
        }
    }

    public void AgilityUp()
    {
        if (data.statPoint >= 1)
        {
            // data.agility++;
            // data.moveSpeed += 0.1f;
            // data.sprintSpeed += 0.18f;
            // data.statPoint -= 1;
            // data.Save();

            data.agility++;
            data.statPoint--;

            float a = 0.1f;

            float baseMove = 6f;
            float baseSprint = 10f;
            float baseAttackSpeed = 0.8f;

            float maxMove = 8f;
            float maxSprint = 12f;
            float maxAttackSpeed = 1.4f;

            float moveDelta = maxMove - baseMove;
            float sprintDelta = maxSprint - baseSprint;
            float attackSpeedDelta = maxAttackSpeed - baseAttackSpeed;

            data.moveSpeed = baseMove + moveDelta * (1f - 1f / (1f + a * data.agility));
            data.sprintSpeed = baseSprint + sprintDelta * (1f - 1f / (1f + a * data.agility));
            data.attackSpeed = baseAttackSpeed + attackSpeedDelta * (1f - 1f / (1f + a * data.agility));

            SaveAllData();
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
            SaveAllData();
        }
    }

    public void DependUp()
    {
        if (data.statPoint >= 1)
        {
            data.depend++;
            // data.dependRate = 1f - Mathf.Exp(-data.depend * 0.05f);
            var a = 0.05f; // 작을수록 증가폭 작아짐.
            data.dependRate = 1f - 1f / (1f + a * data.depend);
            data.statPoint -= 1;
            SaveAllData();
        }
    }

    public void SpaceSkillUp()
    {
        if (data.skillStatPoint >= 1 && spaceSkill.level < spaceSkill.maxLevel)
        {
            spaceSkill.SkillLevelUp();
            data.skillStatPoint--;
            SaveAllData();
        }
    }

    public void CSkillUp()
    {
        if (data.skillStatPoint >= 1 && cSkill.level < cSkill.maxLevel)
        {
            cSkill.SkillLevelUp();
            data.skillStatPoint--;
            SaveAllData();
        }
    }

    public void RSkillUp()
    {
        if (data.skillStatPoint >= 1 && rSkill.level < rSkill.maxLevel)
        {
            rSkill.SkillLevelUp();
            data.skillStatPoint--;
            SaveAllData();
        }
    }

    public void AtkMasterUp()
    {
        if (data.skillStatPoint >= 1 && attackMastery.level < attackMastery.maxLevel)
        {
            attackMastery.SkillLevelUp();
            data.skillStatPoint--;
            SaveAllData();
        }
    }

    public void GuardUp()
    {
        if (data.skillStatPoint >= 1 && guardSkill.level < guardSkill.maxLevel)
        {
            guardSkill.SkillLevelUp();
            data.skillStatPoint--;
            SaveAllData();
        }
    }

    public void ComboAttackUp()
    {
        if (data.skillStatPoint >= 1 && comboAttackSkill.level < comboAttackSkill.maxLevel)
        {
            comboAttackSkill.SkillLevelUp();
            data.skillStatPoint--;
            SaveAllData();
        }
    }

    #endregion

    #region Attack Damage Methods

    // public float RandomAtkDmg()
    // {
    //     var minDamage = (data.attackDamage + weaponDamage) * (0.1f + attackMastery.masteryStat) + 1;
    //     var maxDamage = data.attackDamage + weaponDamage;
    //     return Random.Range(minDamage, maxDamage);
    // }

    // public int AtkDmg()
    // {
    //     return Mathf.FloorToInt(RandomAtkDmg());
    // }

    // public int SkillDmg(float skilldmg)
    // {
    //     // return (int)((AtkDmg() + (data.magic * 0.5f)) * skilldmg);
    //     return (int)((AtkDmg() + data.magic) * skilldmg);
    // }

    public (float min, float max) GetAttackDamage()
    {
        var minDamage = (data.attackDamage + weaponDamage) * (0.1f + attackMastery.masteryStat) + 1;
        var maxDamage = data.attackDamage + weaponDamage;

        return (minDamage, maxDamage);
    }

    public (float min, float max) GetSkillDamage(float skillDamage)
    {
        var minDamage = (data.attackDamage + weaponDamage) * (0.1f + attackMastery.masteryStat) + 1;
        var maxDamage = data.attackDamage + weaponDamage;

        var minSkillDamage = (minDamage + (data.magic * 0.5f) * skillDamage);
        var maxSkillDamage = (maxDamage + (data.magic * 0.5f) * skillDamage);

        return (minSkillDamage, maxSkillDamage);
    }

    #endregion

    #region Coroutine Methods

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

    private IEnumerator HitCoroutine()
    {
        player.isHit = true;

        yield return new WaitForSeconds(0.1f);

        player.isHit = false;
    }

    public IEnumerator BuffDamage()
    {
        data.attackDamage *= 2;

        yield return new WaitForSeconds(cSkill.duration);

        data.attackDamage /= 2;
    }

    #endregion

    public void PlayerDie()
    {
        Debug.Log("Die");
    }

    // void FindWeapons()
    // {
    //     foreach (WeaponDamageData weapon in weapons)
    //     {
    //         if (weapon.weaponCode.Equals(data.weaponCode))
    //         {
    //             this.weaponCode = weapon.weaponCode;
    //             this.weaponDamage = weapon.damageData;
    //             return;
    //         }
    //     }
    // }

    #region Data Save and Load
    public void LoadAllData()
    {
        spaceSkill.LoadSkill();
        cSkill.LoadSkill();
        rSkill.LoadSkill();
        attackMastery.LoadSkill();
        guardSkill.LoadSkill();
        comboAttackSkill.LoadSkill();
        data.Load();

        player.LoadPlayerPosAndRot();
        player.inventory.LoadSlots();
        player.inventory.LoadCoin();

        // FindWeapons();
    }

    public void SaveAllData()
    {
        spaceSkill.SaveSkill();
        cSkill.SaveSkill();
        rSkill.SaveSkill();
        attackMastery.SaveSkill();
        guardSkill.SaveSkill();
        comboAttackSkill.SaveSkill();
        data.Save();

        player.SavePlayerPosAndRot();
        player.inventory.SaveSlots();
        player.inventory.SaveCoin();
    }

    public void DeleteAllData()
    {
        data.DeleteAll();
    }

    IEnumerator LoadDataCoroutine()
    {
        yield return null;

        yield return new WaitUntil(() => player.inventory.slots.Length > 0);

        LoadAllData();
    }
#endregion
}
