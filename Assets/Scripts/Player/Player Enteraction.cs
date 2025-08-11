using UnityEngine;

public class PlayerEnteraction : MonoBehaviour
{
    // [SerializeField] private float maxDistance = 0.1f; // Enteraction distance
    private float pickupSphereRadius = 3f;
    private float enteractiveSphereRadius = 3f;
    // private RaycastHit hitInfo;
    private Transform pickupItem;
    [SerializeField] private LayerMask layerMask;
    private Inventory inventory;
    Player player;

    void Start()
    {
        player = GetComponent<Player>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (player.PickupPressed)
        {
            TryAction();
            player.PickupPressed = false;
        }

        if (player.EnteractionPressed && !DialogueManager.Instance.IsPlaying())
        {
            TryEnteraction();
            player.EnteractionPressed = false;
        }
    }

    public void TryAction()
    {
        if (player.PickupPressed)
        {
            if (CheckItem())
            {
                CanPickUp();
            }
        }
    }

    public void TryEnteraction()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupSphereRadius, layerMask);

        foreach (var hit in hits)
        {
            if (hit.isTrigger) continue;

            NPCBase npc = hit.GetComponent<NPCBase>();
            if (npc != null)
            {
                npc.Enteract();
                return;
            }
        }
    }

    private bool CheckItem()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Collider[] hits = Physics.OverlapSphere(origin, pickupSphereRadius, layerMask);

        float closeDist = float.MaxValue;
        Transform closeItem = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Item") || hit.CompareTag("Coin"))
            {
                // pickupItem = hit.transform;
                // return true;
                float dist = Vector3.Distance(origin, hit.transform.position);
                if (dist < closeDist)
                {
                    closeDist = dist;
                    closeItem = hit.transform;
                }
            }
        }

        if (closeItem != null)
        {
            pickupItem = closeItem;
            return true;
        }

        pickupItem = null;
        return false;
    }

    private void CanPickUp()
    {
        if (pickupItem != null)
        {
            Debug.Log("Tag = " + pickupItem.tag);
            if (pickupItem.tag == "Item")
            {
                ItemPickUp itemPickUp = pickupItem.GetComponent<ItemPickUp>();
                if (itemPickUp != null)
                {
                    if (inventory.AcquireItem(itemPickUp.item))
                    {
                        Destroy(pickupItem.gameObject);
                        SoundManager.Instance.Play2DSound("Pick Up Sound");
                    }
                }
            }

            else if (pickupItem.tag == "Coin")
            {
                CoinPickUp coinPickUp = pickupItem.GetComponent<CoinPickUp>();
                if (coinPickUp != null)
                {
                    inventory.AcquireCoin(coinPickUp.CoinRange());
                    Destroy(pickupItem.gameObject);
                    SoundManager.Instance.Play2DSound("Pick Up Sound");
                }
            }

            pickupItem = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, pickupSphereRadius);
    }
}
