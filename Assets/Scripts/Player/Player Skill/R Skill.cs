using System.Collections;
using UnityEngine;

public class RSkill : Skill
{
    public GameObject effect;
    public GameObject swordPrefab;
    public GameObject decalPrefab;
    public float forwardOffset = 2.8f;
    public float heightOffset = 10f;

    protected override void Awake()
    {
        base.Awake();
        code = 3;
        // level = 0;
        // cooltime = 30f; //30
        // damage = 2f;
        // cost = 30f; //30
        // duration = 0f;
        maxLevel = 5;
    }

    protected override void Start()
    {
        effect.SetActive(false);
        
        LoadSkill();
        SaveSkill();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        // Debug.Log("Level = " + level);
    }

    public override bool CanActivateSkill()
    {
        return base.CanActivateSkill();
    }

    public override void SkillLevelUp()
    {
        if (level == 0)
        {
            level++;
        }
        else
        {
            level++;
            cooltime -= 2f;
            damage += 0.1f;
            cost -= 3f;
            timer = cooltime;
        }

        SaveSkill();
    }

    public override void SaveSkill()
    {
        PlayerPrefs.SetInt(this.code + " Skill Level", this.level);
        PlayerPrefs.SetFloat(this.code + " Skill Cooltime", this.cooltime);
        PlayerPrefs.SetFloat(this.code + " Skill Damage", this.damage);
        PlayerPrefs.SetFloat(this.code + " Skill Cost", this.cost);
        PlayerPrefs.SetFloat(this.code + " Skill Duration", this.duration);
        PlayerPrefs.SetFloat(this.code + " Skill Timer", this.timer);
    }

    public override void LoadSkill()
    {
        this.level = PlayerPrefs.HasKey(this.code + " Skill Level") ? PlayerPrefs.GetInt(this.code + " Skill Level", this.level) : 0;
        this.cooltime = PlayerPrefs.HasKey(this.code + " Skill Cooltime") ? PlayerPrefs.GetFloat(this.code + " Skill Cooltime", this.cooltime) : 30f;
        this.damage = PlayerPrefs.HasKey(this.code + " Skill Damage") ? PlayerPrefs.GetFloat(this.code + " Skill Damage", this.damage) : 1.5f;
        this.cost = PlayerPrefs.HasKey(this.code + " Skill Cost") ? PlayerPrefs.GetFloat(this.code + " Skill Cost", this.cost) : 30f;
        this.duration = PlayerPrefs.HasKey(this.code + " Skill Duration") ? PlayerPrefs.GetFloat(this.code + " Skill Duration", this.duration) : 0f;
        this.timer = PlayerPrefs.HasKey(this.code + " Skill Timer") ? PlayerPrefs.GetFloat(this.code + " Skill Timer") : cooltime;
    }

    protected override void SkillTimer()
    {
        base.SkillTimer();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        SoundManager.Instance.Play2DSound("R Skill Sound");

        StartCoroutine(UseSkillCoroutine());

        Debug.Log("RSkill");
    }

    IEnumerator UseSkillCoroutine()
    {
        Vector3 spawnPos = transform.position + transform.forward * forwardOffset + Vector3.up * heightOffset;

        Debug.Log("Spawn Pos = " + spawnPos);

        ShowAttackRange(spawnPos);

        yield return new WaitForSeconds(0.8f);

        GameObject sword = Instantiate(swordPrefab, spawnPos, Quaternion.identity);
    }

    void ShowAttackRange(Vector3 _pos)
    {
        GameObject decal = Instantiate(decalPrefab, _pos, Quaternion.identity);
        Destroy(decal, 0.8f);
    }
}
