using UnityEngine;
using UnityEngine.UI;

public class PlayerMagicUI : MonoBehaviour
{
    public Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Image>().fillAmount = player.playerStat.data.currentMagic / player.playerStat.data.maxMagic;
    }
}
