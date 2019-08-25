using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Sprite BulletSprite;

    [Tooltip("Seconds")]
    public float BulletLife = 2;

    public virtual void FirePressed(Vector2 direction)
    {
    }

    public virtual void FireHold(Vector2 direction)
    {
    }

    protected GameObject NewBullet()
    {
        var bullet = new GameObject("Bullet");
        bullet.AddComponent<SpriteRenderer>().sprite = BulletSprite;
        bullet.transform.SetParent(transform);
        bullet.transform.position = transform.position;
        bullet.transform.localScale /= 2;
        return bullet.gameObject;
    }

    protected IEnumerator Shoot(GameObject bullet, Vector2 direction)
    {
        while (bullet.gameObject != null)
        {
            bullet.transform.position += (Vector3)direction.normalized;
            yield return null;
        }
    }
}