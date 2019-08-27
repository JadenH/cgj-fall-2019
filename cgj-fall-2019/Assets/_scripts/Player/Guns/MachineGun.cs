using UnityEngine;

public class MachineGun : Gun
{

    public override void FireHold(Vector2 direction)
    {
        StartCoroutine(Shoot(direction));
    }
}