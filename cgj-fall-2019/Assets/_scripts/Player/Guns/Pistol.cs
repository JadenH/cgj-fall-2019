using UnityEngine;

public class Pistol : Gun
{
    public override void FirePressed(Vector2 direction)
    {
        var bullet = Instantiate(BulletPrefab);
        bullet.transform.SetParent(null);
        bullet.transform.position = transform.position;
        StartCoroutine(Shoot(bullet, direction));
        Destroy(bullet, BulletLife);
    }
}