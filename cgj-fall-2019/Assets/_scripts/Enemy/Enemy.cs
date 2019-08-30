using UnityEngine;

public abstract class Enemy : GameBehaviour
{
    public Vector2Int StartCell;
    public Room CurrentRoom;

    public void Initialize(Room room, Vector2Int startCell)
    {
        CurrentRoom = room;
        StartCell = startCell;
        Player.ChangedRoom.AddListener(PlayerChangedRooms);
    }

    protected abstract void PlayerChangedRooms(Room room);
}
