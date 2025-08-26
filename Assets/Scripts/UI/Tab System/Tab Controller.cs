using UnityEngine;

// https://art-life.tistory.com/146
public class TabController : MonoBehaviour
{
    [SerializeField] Sprite btnNormal;
    [SerializeField] Sprite btnSelect;

    TabButtons[] tabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tabs = GetComponentsInChildren<TabButtons>();
        SwitchTab(tabs[0]);
    }

    public void SwitchTab(TabButtons _target)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            bool _isActiveTab = _target == tabs[i];
            tabs[i].GetPanel.SetActive(_isActiveTab);
            tabs[i].ChangeButtonImage(_isActiveTab ? btnSelect : btnNormal);
        }
    }
}
