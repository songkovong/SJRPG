using TMPro;
using UnityEngine;

public class PlayerSkillAttackTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text skillAttackText;
    public int code = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        skillAttackText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // skillAttackText.text = player.playerStat.SkillDamage.ToString();
        if (code == 1)
        {
            skillAttackText.text = player.playerStat.spaceSkill.damage.ToString();
        }
        else if (code == 2)
        {
            // skillAttackText.text = player.playerStat.spaceSkill.damage.ToString();
        }
        else if (code == 3)
        {
            // skillAttackText.text = player.playerStat.spaceSkill.damage.ToString();
        }
        else
        {
            Debug.Log("No Skill");
        }
    }
}
