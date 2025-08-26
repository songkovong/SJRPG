using UnityEngine;
using UnityEngine.UI;

// https://art-life.tistory.com/146
[RequireComponent(typeof(Button))]
public class TabButtons : MonoBehaviour
{
    [SerializeField] GameObject panel;
    Image btnImage;
    public GameObject GetPanel => panel;

    TabController parent;

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(SwitchTab);
        parent = transform.parent.GetComponent<TabController>();
        btnImage = btn.image;
    }

    void SwitchTab()
    {
        parent.SwitchTab(this);
    }

    public void ChangeButtonImage(Sprite _sprite)
    {
        if (btnImage == null) return;
        if (btnImage.sprite != _sprite)
        {
            btnImage.sprite = _sprite;
        }
    }
}
