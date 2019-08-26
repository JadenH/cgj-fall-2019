using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public double CurrentHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void UpdateHealth()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(double amount)
    {
        CurrentHealth -= amount;
        UpdateHealth();
    }
}
