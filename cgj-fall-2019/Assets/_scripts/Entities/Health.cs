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
        StartCoroutine(Damaged());
        UpdateHealth();
    }

    private IEnumerator Damaged()
    {
        Player.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.1f);
        Player.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
