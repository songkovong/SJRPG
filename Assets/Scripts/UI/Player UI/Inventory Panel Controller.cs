using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelController : MonoBehaviour
{
    Player player;
    InputNumber _inputNumber;

    [Header("Buttons")]
    public Button ExitButton;
    public Button OkButton;
    public Button CancelButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.instance;

        if (player == null)
        {
            Debug.Log("Player Found Error");
        }

        _inputNumber = GameObject.FindWithTag("Input Number").GetComponent<InputNumber>();

        ExitButton.onClick.AddListener(PanelExit);
        OkButton.onClick.AddListener(Ok);
        CancelButton.onClick.AddListener(Cancel);
    }

    void PanelExit()
    {
        UIManager.Instance.CloseWindow(gameObject);
    }

    void Ok()
    {
        _inputNumber.OK();
    }

    void Cancel()
    {
        _inputNumber.Cancel();
    }
}
