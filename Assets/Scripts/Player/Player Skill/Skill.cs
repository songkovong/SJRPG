using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    // Skill
    public List<PlayerSkillData> skills = new List<PlayerSkillData>();
    int skillCode;
    public float cooltime { get; private set; }
    float skillDamage;
    float useSkillMagic;

    Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CurrentPlayerSkill()
    {
        foreach (PlayerSkillData skill in skills)
        {
            if (skill.skillCode == player.skillCode)
            {
                this.skillCode = skill.skillCode;
                this.cooltime = skill.cooltimeData;
                this.skillDamage = skill.damageData;
                this.useSkillMagic = skill.magicData;
            }
        }
    }
}
