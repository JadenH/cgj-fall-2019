using UnityEngine;
using System.Collections;

public class Weapon : GameBehaviour
{
    public float Damage = 10;
    public float CoolDownSeconds = 5f;

    private bool _cooldown;

    private void Update()
    {
        if (_cooldown) return;
        if (Vector2.Distance(transform.position, Player.Character.transform.position) < 1.5f)
        {
            _cooldown = true;
<<<<<<< HEAD
            Player.Health.TakeDamage(Damage);
=======
            Player.Health.TakeDamage(Damage, DamageType.Bite);
            StartCoroutine(Cooldown());
>>>>>>> 3e4b506fb5cf2f914b4fafed23fc2eb1dc41e9f5
        }
    }

    private IEnumerator Damaged()
    {
        Player.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.1f);
        Player.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
