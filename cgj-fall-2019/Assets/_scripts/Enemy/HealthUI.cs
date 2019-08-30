using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health Health;

    public Slider Slider;

    // Use this for initialization
    private void Start()
    {
        Health.HealthChanged.AddListener(UpdateUI);
        Slider.maxValue = Health.MaxHealth;
        Slider.minValue = 0;
    }

    private void UpdateUI(float current, float max)
    {
        Slider.value = current;
    }

}
