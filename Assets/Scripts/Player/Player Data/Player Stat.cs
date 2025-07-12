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
    public List<WeaponDamageData> weapons = new List<WeaponDamageData>();
    int weaponCode;
    public float weaponDamage { get; private set; }

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

        // DeleteAllData();

        LoadAllData();
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

        getDamage = (int)(getDamage * (1 - Random.Range(data.dependRate * 0.5f, data.dependRate)));

        if (getDamage == 0) return;

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
            data.attackDamage *= 1.05f;
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

            float maxMove = 8f;
            float maxSprint = 12f;

            float moveDelta = maxMove - baseMove;
            float sprintDelta = maxSprint - baseSprint;

            data.moveSpeed = baseMove + moveDelta * (1f - 1f / (1f + a * data.agility));
            data.sprintSpeed = baseSprint + sprintDelta * (1f - 1f / (1f + a * data.agility));

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

    public float RandomAtkDmg()
    {
        return Random.Range(data.attackDamage * (0.1f + attackMastery.masteryStat), data.attackDamage);
    }

    public int AtkDmg()
    {
        return Mathf.FloorToInt((RandomAtkDmg() + weaponDamage) * 10f);
    }

    public int SkillDmg(float skilldmg)
    {
        return (int)(AtkDmg() * skilldmg);
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
    public void LoadAllData()
    {
        spaceSkill.LoadSkill();
        cSkill.LoadSkill();
        rSkill.LoadSkill();
        attackMastery.LoadSkill();
        guardSkill.LoadSkill();
        comboAttackSkill.LoadSkill();

        data.Load();

        player.inventory.LoadSlots();
        player.inventory.LoadCoin();

        FindWeapons();
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

        player.inventory.SaveSlots();
        player.inventory.SaveCoin();
    }

    public void DeleteAllData()
    {
        data.DeleteAll();
    }
#endregion
}
