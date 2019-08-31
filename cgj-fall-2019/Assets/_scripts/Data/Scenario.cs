using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "scenario_xxxx", menuName = "ScriptableObjects/Scenario", order = 1)]
public class Scenario : ScriptableObject
{
    public string TruthDescription;

    public bool LockedWhileEnemies;
    public EnemySpawn[] EnemySpawns;
    public ItemSpawn[] ItemSpawns;
}

[Serializable]
public class EnemySpawn
{
    public Enemy EnemyPrefab;
    public int Amount;
}

public class ItemSpawn
{
    public Item Item;
    [Range(0, 100)]
    public float Chance;
}