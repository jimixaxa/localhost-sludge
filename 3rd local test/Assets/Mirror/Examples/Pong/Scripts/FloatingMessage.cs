using UnityEngine;
using UnityEngine.UI;

public class FloatingMessage : MonoBehaviour
{
    public float cooldown = 5;
    public float fadeout = 1;
    public float riseSpeed = 12;
    float fade;

    void Start()
    {
        text = GetComponent<Text>();
        fade = fadeout;
    }

    Text text;

    void Update()
    {
        transform.position = transform.position += Vector3.up * Time.deltaTime * riseSpeed;

        if (cooldown <= 0)
        {
            fadeout -= Time.deltaTime;

            if (text != null)
                text.color = new Color(text.color.r, text.color.g, text.color.b, fadeout / fade);

            if (fadeout <= 0)
                Destroy(gameObject);
        }
        else
            cooldown -= Time.deltaTime;
    }
}