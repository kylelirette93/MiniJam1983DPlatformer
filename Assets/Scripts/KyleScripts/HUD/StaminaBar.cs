using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stamina Bar management for HUD.
/// </summary>
public class StaminaBar : MonoBehaviour
{
    Slider staminaSlider;
    TextMeshProUGUI staminaText;

    private void Awake()
    {
        // Cache references.
        staminaSlider = GetComponentInChildren<Slider>();
        staminaText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        // Subscribe to stamina change.
        Stamina.OnChanged += UpdateStaminaUI;
    }
    private void OnDisable()
    {
        // Unsubscribe from stamina change.
        Stamina.OnChanged -= UpdateStaminaUI;
    }
    /// <summary>
    /// Update stamina UI.
    /// </summary>
    /// <param name="percent"></param>
    private void UpdateStaminaUI(float percent)
    {
        // Update slider and text, if stamina changes.
        staminaSlider.value = percent;
        staminaText.text = $"Stamina: {Mathf.RoundToInt(percent * 100)}";
    }
}

/// <summary>
/// Stamina Management class with easy access methods.
/// </summary>
public static class Stamina
{
    // Event for stamina changes, passing percentage.
    public static event Action<float> OnChanged;

    // Stamina values.
    private static float current = 100f;
    private static float max = 100f;

    /// <summary>
    /// Decrease stamina and invoke change event.
    /// </summary>
    /// <param name="amount">The amount to decrease stamina.</param>
    public static void Decrease(float amount)
    {
        current = Mathf.Max(0, current - amount);
        OnChanged?.Invoke(current / max);
    }

    /// <summary>
    /// Increase stamina and invoke change event.
    /// </summary>
    /// <param name="amount">The amount to increase stamina</param>
    public static void Increase(float amount)
    {
        current = Mathf.Min(max, current + amount);
        OnChanged?.Invoke(current / max);
    }

    /// <summary>
    /// Returns current stamina, to see if can dash.
    /// </summary>
    /// <returns></returns>
    public static float GetCurrentStamina()
    {
        return current;
    }
}
