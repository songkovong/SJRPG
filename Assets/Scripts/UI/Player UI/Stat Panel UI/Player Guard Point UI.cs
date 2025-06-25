using TMPro;
using UnityEngine;

public class PlayerGuardPointUI : MonoBehaviour
{
    public Player player;
    TMP_Text magicPointText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        magicPointText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        magicPointText.text = player.playerStat.data.depend.ToString();
    }
}
