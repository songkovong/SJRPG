using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class PlayerEnteraction : MonoBehaviour
{
    // [SerializeField] private float maxDistance = 0.1f; // Enteraction distance
    private float pickupSphereRadius = 3f;
    private float enteractiveSphereRadius = 1f;
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

        if (player.EnteractionPressed)
        {
            TryAction();
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
        if (player.EnteractionPressed)
        {
            Enteraction();
        }
    }

    private void Enteraction()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Collider[] hits = Physics.OverlapSphere(origin, enteractiveSphereRadius, layerMask);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Portal"))
            {
                Debug.Log("Portal");
                hit.GetComponent<Portal>().ActivatePortal();
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
                    inventory.AcquireItem(itemPickUp.item);
                    Destroy(pickupItem.gameObject);
                    // StartCoroutine(MoveToPlayer(pickupItem.gameObject, 0.3f, 1f));
                    // StartCoroutine(MoveToPlayerUp(pickupItem.gameObject, 1f, 0.1f, 0.2f));
                }
            }

            else if (pickupItem.tag == "Coin")
            {
                CoinPickUp coinPickUp = pickupItem.GetComponent<CoinPickUp>();
                if (coinPickUp != null)
                {
                    inventory.AcquireCoin(coinPickUp.CoinRange());
                    Destroy(pickupItem.gameObject);
                    // StartCoroutine(MoveToPlayer(pickupItem.gameObject, 0.3f, 1f));
                    // StartCoroutine(MoveToPlayerUp(pickupItem.gameObject, 1f, 0.1f, 0.2f));
                }
            }

            pickupItem = null;
        }
    }

    // IEnumerator MoveToPlayer(GameObject item, float duration, float height)
    // {
    //     Transform target = transform;

    //     Vector3 startPos = item.transform.position;
    //     Vector3 endPos = target.position;

    //     float _time = 0f;

    //     while (_time < duration)
    //     {
    //         if(item == null) yield break;

    //         _time += Time.deltaTime;
    //         float t = _time / duration;

    //         Vector3 flatPos = Vector3.Lerp(startPos, endPos, t);

    //         float arcY = height * 4f * t * (1f - t);

    //         item.transform.position = new Vector3(flatPos.x, flatPos.y + arcY, flatPos.z);

    //         yield return null;
    //     }

    //     Destroy(item);
    // }

    // IEnumerator MoveToPlayerUp(GameObject item, float upHeight, float upDuration, float duration)
    // {
    //     Vector3 target = transform.position;

    //     Vector3 startPos = item.transform.position;
    //     Vector3 upPos = startPos + Vector3.up * upHeight;

    //     float _time = 0f;

    //     while (_time < upDuration)
    //     {
    //         _time += Time.deltaTime;
    //         float t = _time / upDuration;
    //         item.transform.position = Vector3.Lerp(startPos, upPos, Mathf.SmoothStep(0f, 1f, t));

    //         yield return null;
    //     }

    //     _time = 0f;

    //     while (_time < duration)
    //     {
    //         _time += Time.deltaTime;
    //         float t = _time / duration;
    //         item.transform.position = Vector3.Lerp(upPos, target, t);

    //         yield return null;
    //     }

    //     Destroy(item);
    // }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, pickupSphereRadius);
    }
}
