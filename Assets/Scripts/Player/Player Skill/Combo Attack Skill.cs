using UnityEngine;

public class ComboAttackSkill : Skill
{
    public int playerCombo;
    protected override void Awake()
    {
        base.Awake();
        code = 6;
        maxLevel = 2;

        playerStat = GetComponent<PlayerStat>();
    }

    protected override void Start()
    {
        LoadSkill();

        SaveSkill();
    }

    public override void SaveSkill()
    {
        PlayerPrefs.SetInt(this.code + " Skill Level", this.level);
        PlayerPrefs.SetFloat(this.code + " Skill Cooltime", this.cooltime);
        PlayerPrefs.SetFloat(this.code + " Skill Damage", this.damage);
        PlayerPrefs.SetFloat(this.code + " Skill Cost", this.cost);
        PlayerPrefs.SetFloat(this.code + " Skill Duration", this.duration);
        PlayerPrefs.SetFloat(this.code + " Skill Combo", this.playerCombo);
    }

    public override void LoadSkill()
    {
        this.level = PlayerPrefs.HasKey(this.code + " Skill Level") ? PlayerPrefs.GetInt(this.code + " Skill Level", this.level) : 0;
        this.cooltime = PlayerPrefs.HasKey(this.code + " Skill Cooltime") ? PlayerPrefs.GetFloat(this.code + " Skill Cooltime", this.cooltime) : 0;
        this.damage = PlayerPrefs.HasKey(this.code + " Skill Damage") ? PlayerPrefs.GetFloat(this.code + " Skill Damage", this.damage) : 0;
        this.cost = PlayerPrefs.HasKey(this.code + " Skill Cost") ? PlayerPrefs.GetFloat(this.code + " Skill Cost", this.cost) : 0;
        this.duration = PlayerPrefs.HasKey(this.code + " Skill Duration") ? PlayerPrefs.GetFloat(this.code + " Skill Duration", this.duration) : 0;
        this.playerCombo = PlayerPrefs.HasKey(this.code + " Skill Combo") ? PlayerPrefs.GetInt(this.code + " Skill Combo", this.playerCombo) : 1;
    }

    public override void SkillLevelUp()
    {
        level++;

        if (level == 1 || level == 2)
        {
            playerCombo++;
        }

        SaveSkill();
    }
}
