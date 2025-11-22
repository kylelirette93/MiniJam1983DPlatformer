using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    Slider staminaSlider;
    TextMeshProUGUI staminaText;

    private void Awake()
    {
        staminaSlider = GetComponentInChildren<Slider>();
        staminaText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Stamina.OnChanged += UpdateStaminaBar;
    }
    private void OnDisable()
    {
        Stamina.OnChanged -= UpdateStaminaBar;
    }
    private void UpdateStaminaBar(float percent)
    {
        staminaSlider.value = percent;
        staminaText.text = $"Stamina: {Mathf.RoundToInt(percent * 100)}";
    }
}

public static class Stamina
{
    public static event Action<float> OnChanged;

    private static float current = 100f;
    private static float max = 100f;

    public static void Decrease(float amount)
    {
        current = Mathf.Max(0, current - amount);
        OnChanged?.Invoke(current / max);
    }

    public static void Increase(float amount)
    {
        current = Mathf.Min(max, current + amount);
        OnChanged?.Invoke(current / max);
    }

    public static float GetCurrentStamina()
    {
        return current;
    }
}
