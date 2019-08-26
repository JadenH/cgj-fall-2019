using System.Collections;
using UnityEngine;

public abstract class Gun : GameBehaviour
{
    public GameObject BulletPrefab;

    [Tooltip("Seconds")]
    public float BulletLife = 2;

    public virtual void FirePressed(Vector2 direction)
    {
    }

    public virtual void FireHold(Vector2 direction)
    {
    }

    protected IEnumerator Shoot(GameObject bullet, Vector2 direction)
    {
        while (bullet.gameObject != null)
        {
            var velocity = direction.normalized / 20;

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