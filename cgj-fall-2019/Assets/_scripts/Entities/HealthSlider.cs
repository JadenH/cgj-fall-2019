using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public Health Health;

    public Slider Slider;
    public const float SliderSpeed = 10f;

    // Use this for initialization
    private void Start()
    {
        Health.HealthChanged.AddListener(UpdateUI);
        Slider.maxValue = Health.MaxHealth;
        Slider.minValue = 0;

        UpdateUI(Health.CurrentHealth, Health.MaxHealth);
    }

    private void UpdateUI(float current, float max)
    {
        StopAllCoroutines();
        StartCoroutine(Animate(current));
    }

    private IEnumerator Animate(float dest)
    {
        while (Math.Abs(Slider.value - dest) > 0.1f)
        {
            Slider.value = Mathf.Lerp(Slider.value, dest, Time.deltaTime * SliderSpeed);
            yield return null;
        }
    }

}
