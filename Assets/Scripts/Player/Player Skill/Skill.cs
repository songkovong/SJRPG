using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Skill : MonoBehaviour
{
    // Skill
    int code;
    int level;
    public float cooltime { get; private set; }
    float damage;
    float cost;
    float duration;

    bool canSkill;
    float timer;

    PlayerStat playerStat;

    public virtual void Start()
    {
        canSkill = true;
        timer = cooltime;

        playerStat = GetComponent<PlayerStat>();
    }

    public virtual void Update()
    {
        SkillTimer();
    }

    public virtual float UseSkill()
    {
        canSkill = false;
        timer -= cooltime;
        playerStat.currentMagic -= cost;

        return damage;
    }

    public virtual bool CanActivateSkill()
    {
        return (playerStat.currentMagic >= cost) && canSkill;
    }

    public virtual void SkillTimer()
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
}
