using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Cell Cell;

    public Door TopDoor;
    public Door RightDoor;
    public Door BottomDoor;
    public Door LeftDoor;

    private Dictionary<Direction, Room> NeighborRooms = new Dictionary<Direction, Room>();

    public int LocationX;
    public int LocationY;

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
        return NeighborRooms[direction];
    }

    public void SetNeighborRoom(Direction direction, Room newRoom)
    {
        if (NeighborRooms.ContainsKey(direction))
        {
            throw new UnityException("Room already has a neighbor for direction!!");
        }

        NeighborRooms.Add(direction, newRoom);
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
        return AllDoors().Where(door => !door.IsUsed());
    }

    public bool IsDoorUsed(Direction direction)
    {
        return GetDoorForDirection(direction).IsUsed();
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

    public void MarkDoorAsUsed(Direction direction)
    {
        GetDoorForDirection(direction).MarkAsUsed();
    }

    public void UnlockDoor(Direction direction)
    {
        GetDoorForDirection(direction).UnlockDoor();
    }

    public bool HasAvailableDoor()
    {
        return UnusedDoors().Any();
    }
}