using UnityEngine;

[RequireComponent(typeof(Room))]
public class Spawner : GameBehaviour
{
    private Room _room;
    private bool _spawned = false;

    private void Awake()
    {
        _room = GetComponent<Room>();
        Player.ChangedRoom.AddListener(PlayerEnter);
    }

    private void PlayerEnter(Room room)
    {
        if (_room != room) return;
        Spawn();
    }

    public void Spawn()
    {
        if (!_spawned)
        {
            _spawned = true;
            EnemyManager.CreateEnemies(_room);
        }
    }
}
