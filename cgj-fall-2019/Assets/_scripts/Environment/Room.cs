using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Room : GameBehaviour
{
    public Vector2Int RoomCell;

    public Door TopDoor;
    public Door RightDoor;
    public Door BottomDoor;
    public Door LeftDoor;

    public Tilemap ColliderTilemap;
    public Tilemap RenderTilemap;

    public Spawner Spawner;

    public GameObject Portals;

    private void Awake()
    {
        if (Portals != null) Portals.SetActive(false);
    }

    public void PlayerEnter()
    {
        if (Spawner != null) Spawner.Spawn(this);
    }

    public IEnumerable<Vector2Int> GetLocalCells()
    {
        foreach (var cell in RenderTilemap.cellBounds.allPositionsWithin)
        {
            if (RenderTilemap.HasTile(cell))
            {
                yield return (Vector2Int) cell;
            }
        }
    }

    public IEnumerable<Vector2Int> GetWorldCells()
    {
        foreach (var pos in RenderTilemap.cellBounds.allPositionsWithin)
        {
            if (RenderTilemap.HasTile(pos))
            {
                var world = RenderTilemap.CellToWorld(pos);
                var worldCell = new Vector2Int((int)world.x, (int)world.y);
                yield return worldCell;
            }
        }
    }

    public IEnumerable<Vector3Int> PathableWorldCells()
    {
        foreach (var pos in RenderTilemap.cellBounds.allPositionsWithin)
        {
            if (RenderTilemap.HasTile(pos) && IsPathable((Vector2Int) pos))
            {
                yield return pos;
            }
        }
    }

    public Vector2Int GetRandomPathableCell()
    {
        var cells = PathableWorldCells().ToArray();
        return (Vector2Int) cells[Random.Range(0, cells.Length)];
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
        foreach (var cell in GetLocalCells())
        {
            Gizmos.color = IsPathable((Vector2Int) cell) ? Color.blue : Color.red;
            Gizmos.DrawSphere(RenderTilemap.GetCellCenterWorld((Vector3Int) cell), 0.15f);
        }
    }
}