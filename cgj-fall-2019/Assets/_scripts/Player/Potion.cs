using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion: Item
{
    public float Healing = 10;
    public float DamageIncrease = 10;
    public float Life = 1;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Character>())
        {
            Game.Player.Health.AddHealth(Healing);
            Game.Player.DamageModifier += DamageIncrease;
            Game.Player.Life.CurrentLives += 1;
            Destroy(gameObject);
        }
    }
}
