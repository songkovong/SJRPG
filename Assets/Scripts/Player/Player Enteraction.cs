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
        // Vector3 origin = transform.position + Vector3.up * 0.5f; // 약간 위로 들어 올려서 땅과 겹치지 않게
        // Vector3 direction = transform.forward;

        // return Physics.SphereCast(origin, sphereRadius, direction, out hitInfo, maxDistance, layerMask) &&
        //        hitInfo.transform.CompareTag("Item");

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
        // if (hitInfo.transform != null)
        // {
        //     ItemPickUp itemPickUp = hitInfo.transform.GetComponent<ItemPickUp>();
        //     if (itemPickUp != null)
        //     {
        //         Debug.Log(itemPickUp.item.itemName + " 획득 했습니다.");  // 실제 인벤토리에 넣는 로직으로 대체 필요
        //         Destroy(hitInfo.transform.gameObject);
        //     }
        // }

        if (pickupItem != null)
        {
            ItemPickUp itemPickUp = pickupItem.GetComponent<ItemPickUp>();
            if (itemPickUp != null)
            {
                Debug.Log(itemPickUp.item.itemName + " 획득 했습니다.");  // 실제 인벤토리에 넣는 로직으로 대체 필요
                inventory.AcquireItem(itemPickUp.item);
                Destroy(pickupItem.gameObject);
            }
            pickupItem = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Vector3 origin = transform.position + Vector3.up * 0.5f;
        // Vector3 direction = transform.forward;
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(origin + direction * maxDistance, sphereRadius);

        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, sphereRadius);
    }
}
