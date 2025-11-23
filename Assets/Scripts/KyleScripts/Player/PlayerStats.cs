using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Stat variables.
    private int stamina;
    private int score;
    private float distanceTraveled;

    [SerializeField] private StaminaBar staminaBar;
    private void Start()
    {
        stamina = 100;
        score = 0;
    }

    private void Update()
    {
        distanceTraveled += Time.deltaTime * 5f; 
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

public static class Distance 
{
    // Event for distance changes, passing distance.
    public static event Action<float> OnChanged;
    private static float distance = 0f;
    /// <summary>
    /// Update distance and invoke change event.
    /// </summary>
    /// <param name="amount">The amount to increase distance.</param>
    public static void UpdateDistance(float amount)
    {
        distance += amount;
        OnChanged?.Invoke(distance);
    }
    /// <summary>
    /// Returns current distance traveled.
    /// </summary>
    /// <returns></returns>
    public static float GetDistance()
    {
        return distance;
    }
}

