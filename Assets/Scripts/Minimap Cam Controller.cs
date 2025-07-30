using UnityEngine;

public class MinimapCamController : MonoBehaviour
{
    Player player;

    void Start()
    {
        if (player == null)
        {
            player = Player.instance;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPos = player.transform.position;
            transform.position = newPos;
        }
    }
}
