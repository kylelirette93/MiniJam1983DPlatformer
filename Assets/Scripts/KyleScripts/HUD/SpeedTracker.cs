using TMPro;
using UnityEngine;

public class SpeedTracker : MonoBehaviour
{
    TextMeshProUGUI speedText;

    private void Awake()
    {
        speedText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
         Speed.OnChanged += UpdateSpeedText;
    }

    private void OnDisable()
    {
        Speed.OnChanged -= UpdateSpeedText;
    }

    private void UpdateSpeedText(float speed)
    {
        speedText.text = $"Max Speed Gained: {Mathf.RoundToInt(speed)}";
    }
}
