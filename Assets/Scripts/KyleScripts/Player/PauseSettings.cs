using UnityEngine;
using UnityEngine.UI;

public class PauseSettings : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        AudioManager audioManager = GameManager.Instance.AudioManager;

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);

        volumeSlider.onValueChanged.AddListener(audioManager.UpdateVolume);
    }
}
