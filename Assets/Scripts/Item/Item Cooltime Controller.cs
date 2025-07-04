using System.Collections.Generic;
using UnityEngine;

public class ItemCooltimeController : MonoBehaviour
{
    Dictionary<Item, float> lastUseTime = new Dictionary<Item, float>();

    public bool CanUseItem(Item item)
    {
        if (!lastUseTime.ContainsKey(item))
        {
            return true;
        }
        float lastUse = lastUseTime[item];
        return Time.time - lastUse >= item.cooltime;
    }

    public void UseItem(Item item)
    {
        if (CanUseItem(item))
        {
            lastUseTime[item] = Time.time;
        }
        else
        {
            float remainTime = item.cooltime - (Time.time - lastUseTime[item]);
        }
    }

    public float GetCoolTime(Item item)
    {
        if (!lastUseTime.ContainsKey(item))
        {
            return 0f;
        }

        float remainTime = item.cooltime - (Time.time - lastUseTime[item]);
        return Mathf.Max(0f, remainTime);
    }
}
