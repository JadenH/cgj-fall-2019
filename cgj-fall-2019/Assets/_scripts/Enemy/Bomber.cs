using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Health))]
public class Bomber : Enemy
{
    public float Damage;
    public float Range = 2f;
    public float TimeToExplode = 1f;
    public GameObject ExplosionEffect;

    public Animator Animator;
    public AnimationCurve ExplodeCurve;

    private Mover _mover;
    private Chaser _chaser;
    private Health _health;

    private void Awake()
    {
        _chaser = GetComponent<Chaser>();
        _mover = GetComponent<Mover>();
        _health = GetComponent<Health>();
    }

    protected override void PlayerChangedRooms(Room room)
    {
        if (room == CurrentRoom)
        {
            _chaser.Target = Player.transform;
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        yield return new WaitUntil(() => _mover.Moving);
        yield return new WaitWhile(() => _mover.Moving);
        Animator.SetTrigger("Explode");

        var time = 0f;
        while (time < TimeToExplode)
        {
            Animator.speed = ExplodeCurve.Evaluate(time * 10f);
            time += Time.deltaTime;
            yield return null;
        }

        if (Vector2.Distance(transform.position, Player.Character.transform.position) < Range)
        {
            Player.Health.TakeDamage(Damage);
        }

        var effect = Instantiate(ExplosionEffect, null);
        effect.transform.position = transform.position;
        Destroy(effect, 2);
        Die();
    }
}