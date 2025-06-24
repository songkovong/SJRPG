using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Player player;
    public int code;

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
        string tooltipText = GetSkillTooltip();
        TooltipController.instance?.ShowTooltip(tooltipText, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipController.instance?.HideTooltip();
    }

    string GetSkillTooltip()
    {
        switch (code)
        {
            case 1:
                return "Player Spin Attack " + player.playerStat.spaceSkill.damage * 100 + "% damage.";
            case 2:
                return "Player attack damage is double and duration is " + player.playerStat.cSkill.duration + "seconds.";
            case 3:
                return "Player Strike in Air " + player.playerStat.rSkill.damage * 100 + "% damage.";
            case 4:
                return "Player Attack Mastery increase " + player.playerStat.attackMastery.masteryStat * 100 + "%.";
            case 5:
                return "Player can guard to click mouse right button and mana consume " + player.playerStat.guardSkill.masteryStat + " in 1 second.";
            default:
                return "No Skill Information.";
        }
    }
}
