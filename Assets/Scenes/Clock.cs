using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TextMeshProUGUI clockText;

    public float timer = 0f;
    public bool isRunning = false;

    private void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            int milliseconds = Mathf.FloorToInt((timer * 1000f) % 1000f);

            clockText.text = string.Format(
                "{0:00}:{1:00}:{2:000}",
                minutes,
                seconds,
                milliseconds
            );
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isRunning = true;
        }
    }
}
