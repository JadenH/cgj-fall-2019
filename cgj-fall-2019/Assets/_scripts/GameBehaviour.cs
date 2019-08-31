using UnityEngine;

public abstract class GameBehaviour : MonoBehaviour
{
    public Transform CameraTarget => Game.Instance.CameraTarget;
    public Player Player => Game.Instance.Player;
    public Map Map => Game.Instance.Map;
    public EnemyManager EnemyManager => Game.Instance.EnemyManager;
    public Level CurrentLevel => Game.Instance.CurrentLevel;
    public Game Game => Game.Instance;
    public CameraController CameraController => Game.Instance.CameraController;
    public PlayerLife PlayerLife => Game.Instance.PlayerLife;
}
