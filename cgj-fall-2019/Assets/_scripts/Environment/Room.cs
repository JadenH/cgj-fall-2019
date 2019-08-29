using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public Map Map;
    public Cell Cell;

    public Door TopDoor;
    public Door RightDoor;
    public Door BottomDoor;
    public Door LeftDoor;
    public Tilemap Tilemap;

    public GameObject Portals;


    public Dictionary<Cell, bool> Pathable = new Dictionary<Cell, bool>();

    private void Start()
    {
        SetupPathables();
    }

    private void SetupPathables()
    {
        foreach (var pos in Tilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            var place = Tilemap.CellToWorld(localPlace);
            Debug.Log((Cell)place);
            Pathable.Add((Cell)place, !Tilemap.HasTile(localPlace));
        }
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
        return Map.GetRoomAtCell(Cell.GetNext(direction));
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
}