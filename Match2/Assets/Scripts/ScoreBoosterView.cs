using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ScoreBoosterView : MonoBehaviour
{
    [SerializeField] private Slider _boosterSlider;

    [SerializeField] private TMP_Text _boostText;

    [SerializeField] private ParticleSystem _particleSystem;

    public Slider BoosterSlider => _boosterSlider;

    public void UpdateBoosterText(int amount)
    {
        _boostText.text = $"{amount}x";

        SetMaxParticles(amount);
    }

    public void UpdateSliderValue(float value)
    {
        _boosterSlider.value = value;
    }

    private void SetMaxParticles(int value)
    {
        ParticleSystem.MainModule main = _particleSystem.main;

        main.maxParticles = value;
    }
}
