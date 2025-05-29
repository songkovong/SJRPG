using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    Player player;
    float orbitRadius = 0.8f; // radius
    float orbitDegree; // degree
    float orbitSpeed = 400f; // orbit speed

    public virtual void Start()
    {
        player = GetComponentInParent<Player>();
        EndTrail();
    }

    public virtual void Update() {}

    // https://sharp2studio.tistory.com/4
    public void OrbitRotation()
    {
        orbitDegree += Time.deltaTime * orbitSpeed;

        if (orbitRadius >= 360f) orbitDegree -= 360f;

        float rad = Mathf.Deg2Rad * (orbitDegree);
        float x = orbitRadius * Mathf.Cos(rad);
        float z = orbitRadius * Mathf.Sin(rad);

        transform.position = player.transform.position + new Vector3(x, 0.5f, z);
        // orbitObject.transform.rotation = Quaternion.Euler(0, 0, orbitDegree * -1); // 가운데를 바라보게 각도 조절
        // orbitObject.transform.rotation = Quaternion.LookRotation(this.transform.position - orbitObject.transform.position);
    }

    public void StartTrail() => this.gameObject.SetActive(true);
    public void EndTrail() => this.gameObject.SetActive(false);
}
