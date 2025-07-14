using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string destScene;
    public string destPortalId;
    public string thisPortalId;

    private bool isTransition = false;

    Player player;

    void Awake()
    {
        PortalManager.Instance?.Register(this);
    }

    void Start()
    {
        player = Player.instance;
    }

    void Update()
    {
        player = Player.instance;
        Debug.Log(player.name);
    }

    void OnDestroy()
    {
        PortalManager.Instance.Unregister(this);
    }

    public void ActivatePortal()
    {
        Debug.Log("[Portal] ActivatePortal() 호출됨");
        isTransition = true;
        StartCoroutine(TransitionPlayer());
    }

    IEnumerator TransitionPlayer()
    {
        Debug.Log("[Portal] TransitionPlayer() 시작");
        // yield return StartCoroutine(ScreenFader.FadeOut());

        yield return SceneController.Instance.LoadSceneAsync(destScene);
        // yield return SceneManager.LoadSceneAsync(destScene);

        // yield return new WaitUntil(() => Player.instance != null);
        // player = Player.instance;

        Portal targetPortal = PortalManager.Instance.GetPortalId(destPortalId);

        Debug.Log($"[Portal] player: {(player == null ? "null" : player.name)}");
        Debug.Log($"[Portal] targetPortal: {(targetPortal == null ? "null" : targetPortal.name)}");

        if (targetPortal != null && player != null)
        {
            player.transform.position = targetPortal.transform.position;
            Debug.Log($"[Portal] 이동 완료: {player.transform.position}");
        }
        else
        {
            Debug.Log("Dest portal find error");
        }

        // yield return StartCoroutine(ScreenFader.FadeIn());

        isTransition = false;

    }
}
