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

    public Portal[] Portals;

    private void Awake()
    {
        SetPortalsActive(false);
    }

    private IEnumerable<Vector3Int> GetLocalCells()
    {
        foreach (var cell in RenderTilemap.cellBounds.allPositionsWithin)
        {
            if (RenderTilemap.HasTile(cell))
            {
                yield return cell;
            }
        }
    }

    public IEnumerable<Vector2Int> GetWorldCells()
    {
        return GetLocalCells().Select(LocalCellToWorldCell);
    }

    public IEnumerable<Vector2Int> PathableWorldCells()
    {
        return GetWorldCells().Where(IsPathable);
    }

    public Vector2Int GetRandomPathableCell()
    {
        var cells = PathableWorldCells().ToArray();
        return cells[Random.Range(0, cells.Length)];
    }

    public Vector3Int WorldCellToLocalCell(Vector2Int worldCell)
    {
        return ColliderTilemap.WorldToCell(new Vector3(worldCell.x, worldCell.y));
    }

    public Vector2Int LocalCellToWorldCell(Vector3Int localCell)
    {
        var world = RenderTilemap.CellToWorld((Vector3Int)localCell);
        return new Vector2Int((int)world.x, (int)world.y);
    }

    public bool IsPathable(Vector2Int worldCell)
    {
        return !ColliderTilemap.HasTile(WorldCellToLocalCell(worldCell));
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
        SetPortalsActive(true);
        var randomIndex = Random.Range(0, Portals.Length);
        for (var i = 0; i < Portals.Length; i++)
        {
            var portal = Portals[i];
            if (randomIndex == i)
            {
                portal.SetText(CurrentLevel.RandomLie().TruthDescription);
                portal.Truth = false;
            }
            else
            {
                portal.SetText(CurrentLevel.RandomTruth().TruthDescription);
                portal.Truth = true;
            }
        }
    }

    private void SetPortalsActive(bool val)
    {
        foreach (var portal in Portals)
        {
            portal.gameObject.SetActive(val);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var cell in GetWorldCells())
        {
            Gizmos.color = IsPathable(cell) ? Color.blue : Color.red;
            Gizmos.DrawSphere(Map.GetCellCenter(cell), 0.15f);
        }
    }
}