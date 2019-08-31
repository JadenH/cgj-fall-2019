using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Gun : GameBehaviour
{
    [Tooltip("Seconds")]
    public float Cooldown;
    [HideInInspector]
    public float CurrentCooldown;
    public float BulletLife = 2;
    public GameObject BulletPrefab;
    public double PickupRadius = 1.5;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Start()
    {
        var circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = (float)PickupRadius;
        circleCollider.isTrigger = true;
    }

    public virtual void FirePressed(Vector2 direction)
    {
    }

    public virtual void FireHold(Vector2 direction)
    {
    }

    protected IEnumerator Shoot(Vector2 direction)
    {
        CurrentCooldown = Time.time + Cooldown / 100;
        var bullet = Instantiate(BulletPrefab);
        if (_audioSource != null) _audioSource.Play();
        bullet.transform.SetParent(null);
        bullet.transform.position = transform.position;
        Destroy(bullet, BulletLife);
        while (bullet.gameObject != null)
        {
            var velocity = direction.normalized * Random.Range(.5f, 1.5f) / 4;

            var hit = Physics2D.Raycast(bullet.transform.position, velocity.normalized, velocity.magnitude);
            if (hit.collider && hit.transform.gameObject.tag != "Player")
            {
                if (hit.collider.isTrigger == false || hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (hit.transform.GetComponent<Health>())
                    {
                        hit.transform.GetComponent<Health>().TakeDamage(10, DamageType.Gun);
                        bullet.transform.position = hit.transform.position;
                    }
                    else
                    {
                        bullet.transform.position = hit.point;
                    }
                    bullet.GetComponent<Animator>().SetTrigger("Dead");
                    bullet.transform.localScale *= 2 * Random.Range(.8f, 1.2f);
                    bullet.transform.rotation = Quaternion.AngleAxis(Random.Range(-10, 10), Vector3.back) * Quaternion.LookRotation(Vector3.forward, -hit.normal);
                    Destroy(bullet.gameObject, .5f);
                    break;
                }
            }
            bullet.transform.position += (Vector3) velocity * Time.deltaTime * 100;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, velocity);
            yield return null;
        }
    }
}