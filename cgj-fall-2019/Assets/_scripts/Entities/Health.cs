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
    public class HealthUpdate : UnityEvent<float, float, DamageType> { }

    public HealthUpdate HealthChanged;

    public void TakeDamage(float amount, DamageType damageType)
    {
        CurrentHealth -= amount;
        HealthChanged?.Invoke(CurrentHealth, amount, damageType);
    }

    public void AddHealth(float amount)
    {
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        HealthChanged?.Invoke(CurrentHealth, amount, DamageType.Heal);
    }
}
