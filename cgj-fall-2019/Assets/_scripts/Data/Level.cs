using UnityEngine;
using System.Collections;
using Boo.Lang;

[CreateAssetMenu(fileName = "level_xxxx", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    public Scenario[] PossibleTrueScenarios;
    public Scenario[] PossibleLieScenarios;

    public Scenario RandomLie()
    {
        return PossibleLieScenarios[Random.Range(0, PossibleLieScenarios.Length)];
    }

    public Scenario RandomTruth()
    {
        return PossibleTrueScenarios[Random.Range(0, PossibleTrueScenarios.Length)];
    }
}