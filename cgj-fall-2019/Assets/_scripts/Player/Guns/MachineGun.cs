using UnityEngine;

public class MachineGun : Gun
{
    public override void FireHold(Vector2 direction)
    {
        var bullet = Instantiate(BulletPrefab);
        bullet.transform.SetParent(null);
        bullet.transform.position = transform.position;
        StartCoroutine(Shoot(bullet, direction));
        Destroy(bullet, BulletLife);
    }
}