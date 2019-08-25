using UnityEngine;

namespace Player.Guns
{
    public class Pistol : Gun
    {
        public override void FirePressed(Vector2 direction)
        {
            var bullet = NewBullet();
            StartCoroutine(Shoot(bullet, direction));
            Destroy(bullet, bulletLife);
        }
    }
}
