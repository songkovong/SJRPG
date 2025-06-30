using TMPro;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    public static TooltipController instance;

    public GameObject tooltipPanel;
    public TMP_Text skillNameText;
    public TMP_Text tooltipText;
    bool isVisible = false;

    void Awake()
    {
        instance = this;
        tooltipPanel.SetActive(false);
    }

    public void ShowTooltip(string _name, string text, Vector2 position)
    {
        if (isVisible) return;

        skillNameText.text = _name;
        tooltipText.text = text;

        Vector2 offset = new Vector2(15f, -15f);
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
