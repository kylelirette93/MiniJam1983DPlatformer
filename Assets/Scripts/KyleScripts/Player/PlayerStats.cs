using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Stat variables.
    private int stamina;
    private int score;

    [SerializeField] private StaminaBar staminaBar;
    private void Start()
    {
        stamina = 100;
        score = 0;
    }
}
