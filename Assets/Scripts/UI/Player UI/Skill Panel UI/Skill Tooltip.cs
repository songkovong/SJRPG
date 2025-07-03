using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Player player;
    public int code;
    string _name;
    string _desc;

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
        TooltipController.instance?.ShowTooltip(_name, _desc, Input.mousePosition);
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
                break;
            case 2:
                _name = "Rage";
                _desc = "Player attack damage is double and duration is " + player.playerStat.cSkill.duration + "seconds.";
                break;
            case 3:
                _name = "Strike";
                _desc = "Player Strike in Air " + player.playerStat.rSkill.damage * 100 + "% damage.";
                break;
            case 4:
                _name = "Attack Mastery";
                _desc = "Player Attack Mastery increase " + player.playerStat.attackMastery.masteryStat * 100 + "%.";
                break;
            case 5:
                _name = "Guard";
                _desc = "Player can guard to click mouse right button and mana consume " + player.playerStat.guardSkill.masteryStat + " in 1 second.";
                break;
            default:
                _name = "No Skill";
                _desc = "No Skill Information.";
                break;
        }
    }
}
