using UnityEngine;

public abstract class GameBehaviour : MonoBehaviour
{
    public Transform CameraTarget => Game.Instance.CameraTarget;
    public Player Player => Game.Instance.Player;
    public Map Map => Game.Instance.Map;
    public EnemyManager EnemyManager => Game.Instance.EnemyManager;
    public Level CurrentLevel => Game.Instance.CurrentLevel;
}
