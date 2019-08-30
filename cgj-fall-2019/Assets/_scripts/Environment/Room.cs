using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public Map Map;
    public Vector2Int RoomCell;

    public Door TopDoor;
    public Door RightDoor;
    public Door BottomDoor;
    public Door LeftDoor;

    public Tilemap ColliderTilemap;
    public Tilemap RenderTilemap;

    public GameObject Portals;

    public IEnumerable<Vector3Int> GetCells()
    {
        foreach (var pos in RenderTilemap.cellBounds.allPositionsWithin)
        {
            if (RenderTilemap.HasTile(pos))
            {
                yield return pos;
            }
        }
    }

    public bool IsPathable(Vector2Int worldCell)
    {
        var cell = ColliderTilemap.WorldToCell(new Vector3(worldCell.x, worldCell.y));
        return !ColliderTilemap.HasTile(cell);
    }

    public Door GetDoorForDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return TopDoor;
            case Direction.Right:
                return RightDoor;
            case Direction.Down:
                return BottomDoor;
            case Direction.Left:
                return LeftDoor;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    public Room GetNeighborRoom(Direction direction)
    {
        return Map.GetRoomForRoomCell(RoomCell.GetNextRoomCell(direction));
    }

    public IEnumerable<Door> AllDoors()
    {
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            yield return GetDoorForDirection(direction);
        }
    }

    public IEnumerable<Door> UnusedDoors()
    {
        return AllDoors().Where(door => door.Used == false);
    }

    public bool IsDoorLocked(Direction direction)
    {
        return GetDoorForDirection(direction).IsLocked();
    }

    public void LockDoor(Direction direction)
    {
        GetDoorForDirection(direction).LockDoor();
    }

    public void LockAllDoors()
    {
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            LockDoor(direction);
        }
    }

    public void UnlockAllDoors()
    {
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            UnlockDoor(direction);
        }
    }

    public void UnlockDoor(Direction direction)
    {
        GetDoorForDirection(direction).UnlockDoor();
    }

    public bool HasAvailableDoor()
    {
        return UnusedDoors().Any();
    }

    public void CreatePortals()
    {
        if (Portals != null) Portals.SetActive(true);
    }


    private void OnDrawGizmos()
    {
        foreach (var cell in GetCells())
        {
            Gizmos.color = IsPathable((Vector2Int) cell) ? Color.blue : Color.red;
            Gizmos.DrawSphere(RenderTilemap.GetCellCenterWorld(cell), 0.15f);
        }
    }
}