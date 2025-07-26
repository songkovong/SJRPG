using UnityEngine;

public class SwordDrop : MonoBehaviour
{
    [SerializeField] private float dropSpeed = 40f;
    [SerializeField] private float dropDistance = 8f;

    private float targetY;

    void Start()
    {
        targetY = transform.position.y - dropDistance;   
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (pos.y > targetY)
        {
            pos.y -= dropSpeed * Time.deltaTime;
            if (pos.y < targetY)
            {
                pos.y = targetY;
            }

            transform.position = pos;
        }
        else
        {
            OnLand();
        }
    }

    void OnLand()
    {
        Destroy(gameObject, 2f);
    }
}
