using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Door BottomDoor;
    public Door LeftDoor;
    public Door RightDoor;
    public Door TopDoor;

    public int LocationX;
    public int LocationY;

    Room()
    {

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
    

    public void MarkDoorAsUsed(Direction direction)
    {
        GetDoorForDirection(direction).MarkAsUsed();
    }

    public void UnlockDoor(Direction direction)
    {
        GetDoorForDirection(direction).UnlockDoor();
    }
}