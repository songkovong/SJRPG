using TMPro;
using UnityEngine;

public class PlayerSkillAttackTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text attackRangeText;
    float damage = 0;
    float weaponDamage = 0;
    float mastery = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        attackRangeText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        damage = player.playerStat.data.attackDamage;
        weaponDamage = player.playerStat.weaponDamage;
        mastery = player.playerStat.attackMastery.masteryStat;
        attackRangeText.text = Mathf.FloorToInt((damage * (0.1f + mastery) + weaponDamage) * 10).ToString() + " ~ " + Mathf.FloorToInt((damage + weaponDamage) * 10).ToString();
    }
}
