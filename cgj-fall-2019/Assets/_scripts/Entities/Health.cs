using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : GameBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth = 100;

    [Serializable]
    public class HealthUpdate : UnityEvent<float, float, DamageType> { }

    public HealthUpdate HealthChanged;

    public void TakeDamage(float amount, DamageType damageType)
    {
        CurrentHealth -= amount;
<<<<<<< HEAD
        StartCoroutine(Damaged());
        UpdateHealth();
=======
        HealthChanged?.Invoke(CurrentHealth, amount, damageType);
>>>>>>> 3e4b506fb5cf2f914b4fafed23fc2eb1dc41e9f5
    }

    private IEnumerator Damaged()
    {
        Player.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.1f);
        Player.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
