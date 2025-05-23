using UnityEngine;

public class UIManager : MonoBehaviour
{
    Player player;
    public GameObject statPanel;
    public GameObject itemPanel;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        ShowStat();
        ShowItem();
    }

    void ShowStat()
    {
        if (player.StatPressed)
        {
            statPanel.SetActive(true);
            GameManager.instance.PauseGame();
        }
        else
        {
            statPanel.SetActive(false);
            GameManager.instance.DePauseGame();
        }
    }

    void ShowItem()
    {
        if (player.ItemPressed)
        {
            itemPanel.SetActive(true);
            GameManager.instance.PauseGame();
        }
        else
        {
            itemPanel.SetActive(false);
            GameManager.instance.DePauseGame();
        }
    }
}
