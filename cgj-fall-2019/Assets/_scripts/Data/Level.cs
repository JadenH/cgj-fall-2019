using UnityEngine;
using System.Collections;
using System.Linq;
using Boo.Lang;

[CreateAssetMenu(fileName = "level_xxxx", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    public Scenario[] PossibleScenarios;
    public Scenario SetLieScenario;
}