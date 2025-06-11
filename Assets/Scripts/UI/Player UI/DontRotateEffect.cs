using UnityEngine;

public class DontRotateEffect : MonoBehaviour
{
    void Update()
    {
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
