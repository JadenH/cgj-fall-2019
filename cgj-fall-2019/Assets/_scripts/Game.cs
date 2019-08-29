using UnityEngine;

public class Game : MonoBehaviour
{
    public Transform CameraTarget;
    public Player Player;
    public Map Map;

    public int CurrentLevel = 1;

    #region Singleton

    private static Game _game;
    public const int CellSize = 32;

    public static Game Instance => _game != null ? _game : _game = FindObjectOfType<Game>();

    #endregion

    private void Start()
    {
        StartLevel(CurrentLevel);
    }

    private void StartLevel(int level)
    {
        Map.Generate(level);
        Player.EnterRoom(Map.GetRoomAtCell(Cell.zero));
    }
}