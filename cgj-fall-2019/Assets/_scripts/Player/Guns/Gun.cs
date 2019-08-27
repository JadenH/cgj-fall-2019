using System.Collections;
using UnityEngine;

public abstract class Gun : GameBehaviour
{
    [Tooltip("Seconds")]
    public float Cooldown;
    [HideInInspector]
    public float CurrentCooldown;
    public float BulletLife = 2;
    public GameObject BulletPrefab;
    public double PickupRadius = 1.5;

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
        var bullet = Instantiate(BulletPrefab);
        bullet.transform.SetParent(null);
        bullet.transform.position = transform.position;
        Destroy(bullet, BulletLife);
        while (bullet.gameObject != null)
        {
            var velocity = direction.normalized * Random.Range(.5f, 1.5f) / 4;

            bullet.transform.position += (Vector3) velocity;
            var hit = Physics2D.Raycast(bullet.transform.position, velocity.normalized, velocity.magnitude);
            Debug.DrawRay(bullet.transform.position, velocity.normalized);
            if (hit.collider && hit.transform.gameObject.tag != "Player")
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (hit.transform.GetComponent<Health>())
                    {
                        hit.transform.GetComponent<Health>().TakeDamage(10);
                    }
                }
                Destroy(bullet.gameObject);
            }
            yield return null;
        }
    }
}