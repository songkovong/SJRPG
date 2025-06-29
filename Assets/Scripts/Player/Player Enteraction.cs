using UnityEngine;

public class PlayerEnteraction : MonoBehaviour
{
    // [SerializeField] private float maxDistance = 0.1f; // Enteraction distance
    private float sphereRadius = 3f;
    // private RaycastHit hitInfo;
    private Transform pickupItem;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Inventory inventory;

    Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        TryAction();
    }

    public void TryAction()
    {
        Debug.Log(player.EnteractionPressed);
        if (player.EnteractionPressed)
        {
            if (CheckItem())
            {
                CanPickUp();
            }
        }
    }

    private bool CheckItem()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        Collider[] hits = Physics.OverlapSphere(origin, sphereRadius, layerMask);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Item"))
            {
                pickupItem = hit.transform;
                return true;
            }
        }

        pickupItem = null;
        return false;
    }

    private void CanPickUp()
    {
        if (pickupItem != null)
        {
            ItemPickUp itemPickUp = pickupItem.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                inventory.AcquireItem(itemPickUp.item);
                Destroy(pickupItem.gameObject);
            }
            pickupItem = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, sphereRadius);
    }
}
