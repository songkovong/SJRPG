using TMPro;
using UnityEngine;

public class PlayerAttackTextUI : MonoBehaviour
{
    public Player player;
    TMP_Text attackText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        attackText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        attackText.text = player.playerStat.attackDamage.ToString();
    }
}
