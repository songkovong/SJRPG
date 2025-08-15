using UnityEngine;
using UnityEngine.UI;

public class MapPanelController : MonoBehaviour
{
    public Button ExitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ExitButton.onClick.AddListener(PanelExit);
    }

    void PanelExit()
    {
        UIManager.Instance.CloseWindow(gameObject);
    }
}
