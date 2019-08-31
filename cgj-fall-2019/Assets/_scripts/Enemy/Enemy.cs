using UnityEngine;

public abstract class Enemy : GameBehaviour
{
    public Vector2Int StartCell;
    public Room CurrentRoom;

    public virtual void Initialize(Room room, Vector2Int startCell)
    {
        transform.SetParent(room.transform, true);

        CurrentRoom = room;
        StartCell = startCell;

        Player.ChangedRoom.AddListener(PlayerChangedRooms);
        PlayerChangedRooms(room);
    }

    protected abstract void PlayerChangedRooms(Room room);
}
