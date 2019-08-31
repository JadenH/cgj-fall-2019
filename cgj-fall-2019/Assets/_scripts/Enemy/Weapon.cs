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
            Player.Health.TakeDamage(Damage * Game.Multiplier(), DamageType.Bite);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(CoolDownSeconds);
        _cooldown = false;
    }
}
