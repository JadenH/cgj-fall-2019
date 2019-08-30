using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Chaser))]
public class Bat : GameBehaviour
{
    public Chaser Chaser;
    public Health Health;

    private void Start()
    {
        Chaser.Target = Player.transform;
    }
}
