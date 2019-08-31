using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion: Item
{
    public float Healing = 10;
    public float DamageIncrease = 10;
    public float HealthIncrease = 10;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Character>())
        {
            Debug.Log("GOT POTION");
            Destroy(gameObject);
        }
    }
}
