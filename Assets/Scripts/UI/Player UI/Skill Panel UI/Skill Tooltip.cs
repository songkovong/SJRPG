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
                return "Player Rage and get Damage " + player.playerStat.cSkill.damage * 100 + "%.";
            case 3:
                return "Player Strike in Air " + player.playerStat.rSkill.damage * 100 + "% damage.";
            default:
                return "No Skill Information.";
        }
    }
}
