using TMPro;
using UnityEngine;

public class StatTooltipController : MonoBehaviour
{
    public static StatTooltipController instance;

    public GameObject tooltipPanel;
    public TMP_Text tooltipText;
    bool isVisible = false;

    void Awake()
    {
        instance = this;
        tooltipPanel.SetActive(false);
    }

    public void ShowTooltip(string _text, Vector2 position)
    {
        if (isVisible) return;

        tooltipText.text = _text;

        Vector2 offset = new Vector2(20f, -15f);
        tooltipPanel.transform.position = position + offset;

        tooltipPanel.SetActive(true);

        isVisible = true;
    }

    public void HideTooltip()
    {
        if (!isVisible) return;

        tooltipPanel.SetActive(false);

        isVisible = false;
    }
}
