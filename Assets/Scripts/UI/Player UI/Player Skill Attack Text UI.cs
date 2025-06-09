using TMPro;
using UnityEngine;

public class PlayerSkillAttackTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text skillAttackText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        skillAttackText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        skillAttackText.text = player.playerStat.SkillDamage.ToString();
    }
}
