using System.Linq;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Transform CameraTarget;
    public Player Player;
    public Map Map;
    public EnemyManager EnemyManager;
    public TextMeshProUGUI LevelText;

    public int CurrentLevelNumber = 1;
    public Level CurrentLevel;

    private int _currentLie = 0;
    private int _currentTruth = 0;
    private int[] _randomLies;
    private int[] _randomTruths;

    #region Singleton

    private static Game _game;
    public const int CellSize = 32;

    public static Game Instance => _game != null ? _game : _game = FindObjectOfType<Game>();

    #endregion

    private void Start()
    {
        StartLevel(CurrentLevelNumber);
    }

    private void StartLevel(int levelNumber)
    {
        Map.DestroyLevel();

        var levelName = $"level_{levelNumber.ToString().PadLeft(4, '0')}";
        var level = Resources.Load<Level>($"Levels/{levelName}");

        if (level)
        {
            Debug.Log($"Loaded Level: {levelName}");
            InitializeScenarios(level);
            StartLevel(levelNumber, level);
        }
        else
        {
            Debug.Log($"Loaded Default Level");
            level = Resources.Load<Level>($"Levels/level_0000");
            InitializeScenarios(level);
            StartLevel(levelNumber, level);
        }
    }

    private void InitializeScenarios(Level level)
    {
        if (level.PossibleLieScenarios == null || !level.PossibleLieScenarios.Any())
        {
            Debug.LogError("Level is missing lies!");
        }

        if (level.PossibleTrueScenarios == null || !level.PossibleTrueScenarios.Any())
        {
            Debug.LogError("Level is missing truths!");
        }

        _randomLies = Randomizer(level.PossibleLieScenarios.Length);
        _randomTruths = Randomizer(level.PossibleTrueScenarios.Length);
        _currentLie = 0;
        _currentTruth = 0;
    }

    private void StartLevel(int levelNumber, Level level)
    {
        CurrentLevel = level;
        LevelText.text = levelNumber.ToString();
        Map.Generate(levelNumber);
        Player.EnterRoom(Map.GetRoomForRoomCell(Vector2Int.zero));
        Player.transform.position = Vector3.zero;
    }

    private int[] Randomizer(int max)
    {
        var arr = new int[max];
        for (var i = 0; i < max; i++)
        {
            arr[i] = i;
        }

        return arr.OrderBy(i => Random.Range(0, 1f)).ToArray();
    }

    public Scenario RandomLie()
    {
        var scenario = CurrentLevel.PossibleLieScenarios[_randomLies[_currentLie]];
        _currentLie++;
        if (_currentLie > CurrentLevel.PossibleLieScenarios.Length) _currentLie = 0;
        return scenario;
    }

    public Scenario RandomTruth()
    {
        var scenario = CurrentLevel.PossibleTrueScenarios[_randomTruths[_currentTruth]];
        _currentTruth++;
        if (_currentTruth > CurrentLevel.PossibleLieScenarios.Length) _currentTruth = 0;
        return scenario;
    }

    public void ChooseScenario(Scenario scenario)
    {
        if (CurrentLevel.PossibleLieScenarios.Contains(scenario))
        {
            // Correct
            Debug.Log("CORRECT");
            StartLevel(++CurrentLevelNumber);
        }
        else
        {
            // Incorrect
            Debug.Log("INCORRECT");
        }
    }
}