using UnityEngine;

public class DontRotate : MonoBehaviour
{
    void Update()
    {
        this.gameObject.transform.eulerAngles = new Vector3(45, 0, 0);
    }
}