using UnityEngine;
using UnityEngine.EventSystems;

public class StatTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Player player;
    public int code;
    string _desc;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.GetComponent<Player>();

        if (player == null)
        {
            return;
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetSkillTooltip();
        StatTooltipController.instance?.ShowTooltip(_desc,Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StatTooltipController.instance?.HideTooltip();
    }

    void GetSkillTooltip()
    {
        switch (code)
        {
            case 1:
                _desc = "Player Max Health and Attack Damage is Up.";
                break;
            case 2:
                _desc = "Player Speed and Sprint Speed is Up.";
                break;
            case 3:
                _desc = "Player Max Mana and Mana Recovery Rate Up.";
                break;
            case 4:
                _desc = "If Player Hit by enemy Getting Damage by enemy is lower.";
                break;
            default:
                _desc = "No Stat Information.";
                break;
        }
    }
}
