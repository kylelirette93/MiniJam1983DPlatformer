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


