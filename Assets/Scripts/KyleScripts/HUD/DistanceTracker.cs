using TMPro;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    TextMeshProUGUI distanceText;

    private void Awake()
    {
        distanceText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Distance.OnChanged += UpdateDistanceText;
    }

    private void OnDisable()
    {
        Distance.OnChanged -= UpdateDistanceText;
    }

    private void UpdateDistanceText(float distance)
    {
        distanceText.text = $"Distance: {Mathf.RoundToInt(distance)} m";
    }
}
