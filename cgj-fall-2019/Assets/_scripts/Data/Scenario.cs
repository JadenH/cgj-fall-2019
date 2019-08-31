using System;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "scenario_xxxx", menuName = "ScriptableObjects/Scenario", order = 1)]
public class Scenario : ScriptableObject
{
    public string TruthDescription;

    public EnemySpawn[] EnemySpawns;
}

[Serializable]
public class EnemySpawn
{
    public Enemy EnemyPrefab;
    public int Amount;
}