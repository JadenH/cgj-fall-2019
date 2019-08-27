using UnityEngine;

public class Pistol : Gun
{
    public override void FirePressed(Vector2 direction)
    {
        StartCoroutine(Shoot(direction));
    }
}