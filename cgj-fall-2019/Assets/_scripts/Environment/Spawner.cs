using UnityEngine;
using System.Collections;

public class Spawner : GameBehaviour
{
    private bool _spawned = false;

    public void Spawn(Room room)
    {
        if (!_spawned)
        {
            EnemyManager.CreateEnemy(room);
        }
    }
}
