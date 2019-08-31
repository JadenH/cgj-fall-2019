using UnityEngine;
using System.Collections;

public class EnemyManager : GameBehaviour
{
    public Enemy[] EnemyPrefabs;

    public void CreateEnemy(Room room)
    {
        var randomEnemy = EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)];
        var randomCell = room.GetRandomPathableCell();
        var newEnemy = Instantiate(randomEnemy, Map.GetCellCenter(randomCell), Quaternion.identity);
        newEnemy.Initialize(room, randomCell);
    }
}
