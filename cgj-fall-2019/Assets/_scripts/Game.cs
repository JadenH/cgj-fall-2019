using UnityEngine;

public class Game : MonoBehaviour
{
    public Transform CameraTarget;
    public Player Player;
    public Map Map;
    public EnemyManager EnemyManager;

    public int CurrentLevelNumber = 1;
    public Level CurrentLevel;

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
        var levelName = $"level_{levelNumber.ToString().PadLeft(4, '0')}";
        var level = Resources.Load<Level>($"Levels/{levelName}");

        if (level)
        {
            Debug.Log($"Loaded Level: {levelName}");
            StartLevel(CurrentLevelNumber, level);
        }
        else
        {
            Debug.Log($"Loaded Default Level");
            level = Resources.Load<Level>($"Levels/level_0000");
            StartLevel(CurrentLevelNumber, level);
        }
    }

    private void StartLevel(int levelNumber, Level level)
    {
        CurrentLevel = level;
        Map.Generate(levelNumber);
        Player.EnterRoom(Map.GetRoomForRoomCell(Vector2Int.zero));
    }
}