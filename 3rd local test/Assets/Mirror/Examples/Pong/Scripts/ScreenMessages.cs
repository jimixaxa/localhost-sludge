using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ScreenMessageEvent : UnityEvent<string, Color> { }

public class ScreenMessages : MonoBehaviour
{
    public static UnityEvent ClearMessages = new UnityEvent();
    public static ScreenMessageEvent AddMessage = new ScreenMessageEvent();

    [SerializeField] private GameObject FloatingTextPrefab = null;
    [SerializeField] private float Gap = 2f;

    float messagesTime = 0f;

    private void Awake()
    {
        ClearMessages.AddListener(() =>
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        });

        AddMessage.AddListener((text, color) =>
        {
            if (string.IsNullOrEmpty(text))
                return;

            //move previous messages up to make room
            if (messagesTime > 0f)
                foreach (FloatingMessage m in GetComponentsInChildren<FloatingMessage>())
                    m.transform.position += Vector3.up * messagesTime * m.riseSpeed;

            messagesTime = Gap;

            GameObject message = Instantiate(FloatingTextPrefab);

            Text t = message.GetComponent<Text>();
            t.text = text;
            t.color = color;

            message.transform.SetParent(transform);
            message.transform.localPosition = Vector3.zero;
        });
    }

    void Update()
    {
        if (messagesTime > 0f)
            messagesTime -= Time.deltaTime;

        if (messagesTime < 0f)
            messagesTime = 0f;
    }
}
