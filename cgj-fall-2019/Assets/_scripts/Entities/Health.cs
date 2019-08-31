using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth = 100;

    [Serializable]
    public class HealthUpdate : UnityEvent<float, float> { }

    public HealthUpdate HealthChanged;

    // Start is called before the first frame update

    private void UpdateHealth()
    {
        HealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        UpdateHealth();
    }
}
