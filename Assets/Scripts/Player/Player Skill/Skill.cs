using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Skill : MonoBehaviour
{
    // Skill
    public int code { get; protected set; }
    public int level { get; protected set; }
    public float cooltime { get; protected set; }
    public float damage { get; protected set; }
    public float cost { get; protected set; }
    public float duration { get; protected set; }
    public float maxLevel { get; protected set; }

    public bool canSkill { get; protected set; }
    public float timer { get; protected set; }

    public PlayerStat playerStat { get; protected set; }
    public SkillHitBox hitBox;

    protected virtual void Awake() { }

    protected virtual void Start()
    {
        canSkill = true;
        timer = cooltime;

        playerStat = GetComponent<PlayerStat>();
    }

    protected virtual void Update()
    {
        SkillTimer();
    }

    public virtual void UseSkill()
    {
        canSkill = false;
        timer -= cooltime;
        playerStat.data.currentMagic -= cost;
    }

    public virtual bool CanActivateSkill()
    {
        return (playerStat.data.currentMagic >= cost) && canSkill && !IsLevel0();
    }

    protected virtual void SkillTimer()
    {
        if (!canSkill)
        {
            timer += Time.deltaTime;
            if (timer >= cooltime)
            {
                canSkill = true;
                timer = cooltime;
            }
        }
    }

    public virtual void SkillLevelUp() { }

    public virtual bool IsLevel0()
    {
        if (level == 0) return true;
        else return false;
    }

    public virtual void SaveSkill()
    {
        PlayerPrefs.SetInt(code + " Skill Level", level);
        PlayerPrefs.SetFloat(code + " Skill Cooltime", cooltime);
        PlayerPrefs.SetFloat(code + " Skill Damage", damage);
        PlayerPrefs.SetFloat(code + " Skill Cost", cost);
        PlayerPrefs.SetFloat(code + " Skill Duration", duration);
    }
    public virtual void LoadSkill()
    {
        level = PlayerPrefs.HasKey(code + " Skill Level") ? PlayerPrefs.GetInt(code + " Skill Level", level) : level;
        cooltime = PlayerPrefs.HasKey(code + " Skill Cooltime") ? PlayerPrefs.GetFloat(code + " Skill Cooltime", cooltime) : cooltime;
        cooltime = PlayerPrefs.HasKey(code + " Skill Damage") ? PlayerPrefs.GetFloat(code + " Skill Damage", damage) : damage;
        cooltime = PlayerPrefs.HasKey(code + " Skill Cost") ? PlayerPrefs.GetFloat(code + " Skill Cost", cost) : cost;
        cooltime = PlayerPrefs.HasKey(code + " Skill Duration") ? PlayerPrefs.GetFloat(code + " Skill Duration", duration) : duration;
    }

    public virtual SkillHitBox HitBox => hitBox;
}
