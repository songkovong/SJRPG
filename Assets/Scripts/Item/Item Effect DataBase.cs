using UnityEngine;

public class ItemEffectDataBase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] effects;
    [SerializeField]
    private SlotTooltip tooltip;

    private const string HP = "HP", MP = "MP", DAMAGE = "DAMAGE";

    Player player;
    // Weapon

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        tooltip = GameObject.FindWithTag("Item Tooltip").GetComponent<SlotTooltip>();
    }

    public void UseItem(Item _item)
    {
        if (_item.itemType == Item.ItemType.Equipment)
        {
            WeaponManager.Instance.EquipWeapon(_item);
        }

        if (_item.itemType == Item.ItemType.Consumable)
        {
            for (int i = 0; i < effects.Length; i++)
            {
                if (effects[i].itemName == _item.itemName)
                {
                    for (int j = 0; j < effects[i].parts.Length; j++)
                    {
                        switch (effects[i].parts[j])
                        {
                            case HP:
                                player.playerStat.Heal(effects[i].nums[j]);
                                break;
                            case MP:
                                player.playerStat.MPHeal(effects[i].nums[j]);
                                break;
                            case DAMAGE:
                                //
                                break;
                            default:
                                // Debug.Log("잘못된 Status 부위. HP, SP, DP, HUNGRY, THIRSTY, SATISFY 만 가능합니다.");
                                break;
                        }
                        Debug.Log(_item.itemName + " 을 사용했습니다.");
                    }
                    return;
                }
            }
            Debug.Log("itemEffectDatabase에 일치하는 itemName이 없습니다.");
        }
    }

    public void ShowTooltip(Item _item, Vector3 _pos)
    {
        tooltip.ShowTooltip(_item, _pos);
    }

    public void HideTooltip()
    {
        tooltip.HideTooltip();
    }
}
