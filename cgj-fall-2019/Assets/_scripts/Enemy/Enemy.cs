using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Enemy : GameBehaviour
{
    public Vector2Int StartCell;
    public Room CurrentRoom;

    protected abstract void PlayerChangedRooms(Room room);

    public virtual void Initialize(Room room, Vector2Int startCell)
    {
        transform.SetParent(room.transform, true);

        CurrentRoom = room;
        StartCell = startCell;

        GetComponent<Health>().HealthChanged.AddListener(HealthChanged);
        CurrentRoom.EnemyCreated();

        Player.ChangedRoom.AddListener(PlayerChangedRooms);
        PlayerChangedRooms(room);
    }

    private void HealthChanged(float current, float delta, DamageType damageType)
    {
        if (current <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        CurrentRoom.EnemyDied();
        Destroy(gameObject);
    }
}
