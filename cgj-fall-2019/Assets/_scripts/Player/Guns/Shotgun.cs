using UnityEngine;

public class Shotgun : Gun
{
    public int BulletCount = 20;
    public float Spread = 10;

    public override void FirePressed(Vector2 direction)
    {
        for (var i = 0; i < BulletCount; i++)
        {
            var randomDir = Quaternion.AngleAxis(Random.Range(-Spread, Spread), Vector3.back) * direction;
            StartCoroutine(Shoot(randomDir.normalized));
        }
    }
}