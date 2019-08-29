using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth = 100;

    // Start is called before the first frame update

    private void UpdateHealth()
    {
        if (CurrentHealth <= 0)
        {
            IsDead();
        }
    }

    public virtual void IsDead()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        UpdateHealth();
    }
}
