using TMPro;
using UnityEngine;

public class SkillStatPointUI : MonoBehaviour
{
    Player player;
    TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player")?.GetComponent<Player>();
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = player.playerStat.data.skillStatPoint + " SP";
    }
}
