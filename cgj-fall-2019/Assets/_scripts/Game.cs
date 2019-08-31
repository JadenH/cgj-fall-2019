using System.Collections.Generic;
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
    public CameraController CameraController;
    public AnimationCurve DifficultyCurve;
    public PlayerLife PlayerLife;

    public int CurrentLevelNumber = 1;
    public Level CurrentLevel;

    private Scenario _lieScenario;
    private List<Scenario> _scenarios;
    private int _currentScenario = 0;

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
        if (level.PossibleScenarios == null || !level.PossibleScenarios.Any())
        {
            Debug.LogError("Level is missing scenarios!");
        }
        CurrentLevel = level;

        _scenarios = level.PossibleScenarios.OrderBy(scenario => Random.Range(0f, 1f)).ToList();
        SetupLieScenario();

        _currentScenario = 0;
    }

    private void StartLevel(int levelNumber, Level level)
    {
        LevelText.text = levelNumber.ToString();
        Map.Generate(levelNumber);
        Player.EnterRoom(Map.GetRoomForRoomCell(Vector2Int.zero));
        Player.transform.position = Vector3.zero;
    }

    private void SetupLieScenario()
    {
        if (CurrentLevel.SetLieScenario != null)
        {
            _lieScenario = CurrentLevel.SetLieScenario;
        }
        else
        {
            _lieScenario = _scenarios[Random.Range(0, _scenarios.Count)];
        }

        // Remove it from the pool
        if (_scenarios.Contains(_lieScenario))
        {
            _scenarios.Remove(_lieScenario);
        }
    }

    public Scenario RandomLie()
    {
        return _lieScenario;
    }

    public Scenario RandomTruth()
    {
        var scenario = _scenarios[_currentScenario];
        _currentScenario++;
        if (_currentScenario >= _scenarios.Count) _currentScenario = 0;
        return scenario;
    }

    public void ChooseScenario(Scenario scenario)
    {
        if (_lieScenario == scenario)
        {
            // Correct
            Debug.Log("CORRECT");
            StartLevel(++CurrentLevelNumber);
        }
        else
        {
            // Incorrect
            Debug.Log("INCORRECT");
            PlayerLife.CurrentLives--;
            CurrentLevelNumber = Mathf.Max(1, CurrentLevelNumber - 5);
            StartLevel(CurrentLevelNumber);
        }
    }

    public float Multiplier()
    {
        return CurrentLevelNumber <= 20 ? DifficultyCurve.Evaluate(CurrentLevelNumber) : CurrentLevelNumber;
    }
}