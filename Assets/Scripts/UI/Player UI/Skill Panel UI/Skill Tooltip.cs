using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Player player;
    public int code;
    string _name;
    string _desc;
    string _master;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.GetComponent<Player>();

        if (player == null)
        {
            // text.text = "Player Not Found";
            return;
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetSkillTooltip();
        TooltipController.instance?.ShowTooltip(_name, _desc, _master, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipController.instance?.HideTooltip();
    }

    void GetSkillTooltip()
    {
        switch (code)
        {
            case 1:
                _name = "Slash";
                _desc = "Player Spin Attack " + player.playerStat.spaceSkill.damage * 100 + "% damage.";
                _master = "Master Level: " + player.playerStat.spaceSkill.maxLevel.ToString();
                break;
            case 2:
                _name = "Rage";
                _desc = "Player attack damage is double and Recover Health 5% of the Damages and duration is " + player.playerStat.cSkill.duration + " seconds.";
                _master = "Master Level: " + player.playerStat.cSkill.maxLevel.ToString();
                break;
            case 3:
                _name = "Strike";
                _desc = "Player Strike in Air " + player.playerStat.rSkill.damage * 100 + "% damage.";
                _master = "Master Level: " + player.playerStat.rSkill.maxLevel.ToString();
                break;
            case 4:
                _name = "Attack Mastery";
                _desc = "Player mininum attack range increase " + player.playerStat.attackMastery.masteryStat * 100 + "%.";
                _master = "Master Level: " + player.playerStat.attackMastery.maxLevel.ToString();
                break;
            case 5:
                _name = "Guard";
                _desc = "Player can guard to click mouse right button and mana consume " + player.playerStat.guardSkill.masteryStat.ToString("F2") + " in 1 second.";
                _master = "Master Level: " + player.playerStat.guardSkill.maxLevel.ToString();
                break;
            case 6:
                _name = "Combo Attack";
                _desc = "If skill level up, Player can Combo Attack and Attack Damage is increase. \nCombo 1: Attack Damage * 1\nCombo 2: Attack Damge * 1.25\nCombo 3: Attack Damge * 1.5";
                _master = "Master Level: " + player.playerStat.comboAttackSkill.maxLevel.ToString();
                break;
            default:
                _name = "No Skill";
                _desc = "No Skill Information.";
                _master = "No Skill";
                break;
        }
    }
}
