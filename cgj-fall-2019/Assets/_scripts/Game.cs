using UnityEngine;

public class Game : MonoBehaviour
{
    public Transform CameraTarget;
    public Player Player;

    #region Singleton

    private static Game _game;

    public static Game Instance => _game != null ? _game : _game = FindObjectOfType<Game>();

    #endregion
}