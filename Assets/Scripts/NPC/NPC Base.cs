using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
    [SerializeField] protected string npcID;
    public abstract void Enteract();

    public bool isPlayerDetect { get; protected set; } = false;
    public GameObject EnteractionIcon;

    Player player;
    Quaternion originRot;

    protected virtual void Start()
    {
        EnteractionIcon.SetActive(false);
        player = Player.instance;
        originRot = transform.rotation;
    }

    protected virtual void Update()
    {
        if (isPlayerDetect && player != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 4f);
            }
        }

        else if (!isPlayerDetect)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originRot, Time.deltaTime * 2f);
            if (Quaternion.Angle(transform.rotation, originRot) < 0.1f)
            {
                transform.rotation = originRot;
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        isPlayerDetect = true;
        EnteractionIcon.SetActive(true);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        isPlayerDetect = false;
        EnteractionIcon.SetActive(false);

        if (DialogueManager.Instance != null && DialogueManager.Instance.IsPlaying())
        {
            DialogueManager.Instance.CloseDialogue();
        }
        ShopManager.Instance.CloseShop();
    }
}
