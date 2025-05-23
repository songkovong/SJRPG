using TMPro;
using UnityEngine;

// https://lesslate.github.io/unity/%EC%9C%A0%EB%8B%88%ED%8B%B0-%ED%94%8C%EB%A1%9C%ED%8C%85-%EB%8D%B0%EB%AF%B8%EC%A7%80-%ED%85%8D%EC%8A%A4%ED%8A%B8/
public class DamageText : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    TextMeshPro text;
    Color alpha;
    public float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;

        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text = damage.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

        // UI to any angle 
        transform.forward = Camera.main.transform.forward;

        alpha.a = Mathf.Lerp(alpha.a, 0, alphaSpeed * Time.deltaTime);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
