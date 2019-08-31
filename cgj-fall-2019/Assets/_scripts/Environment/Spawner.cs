using UnityEngine;

[RequireComponent(typeof(Room))]
public class Spawner : GameBehaviour
{
    private Room _room;
    private bool _spawned = false;
    public Scenario Scenario;

    private void Awake()
    {
        _room = GetComponent<Room>();
        Player.ChangedRoom.AddListener(PlayerEnter);
        Scenario = Game.RandomTruth();
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
            foreach (var spawn in Scenario.EnemySpawns)
            {
                for (int i = 0; i < spawn.Amount; i++)
                {
                    SpawnEnemy(spawn);
                }
            }
        }
    }

    private void SpawnEnemy(EnemySpawn spawn)
    {
        var randomCell = _room.GetRandomPathableCell();
        var newEnemy = Instantiate(spawn.EnemyPrefab, Map.GetCellCenter(randomCell), Quaternion.identity);
        newEnemy.Initialize(_room, randomCell);
    }
}
