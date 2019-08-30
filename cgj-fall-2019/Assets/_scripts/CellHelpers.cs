using System;
using UnityEngine;
using System.Collections;

public static class CellHelpers
{
    public static Vector2Int GetNextRoomCell(this Vector2Int cell, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return cell + Vector2Int.up;
            case Direction.Right:
                return cell + Vector2Int.right;
            case Direction.Down:
                return cell + Vector2Int.down;
            case Direction.Left:
                return cell + Vector2Int.left;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    public static Vector2 RoomPosition(this Vector2Int cell)
    {
        return new Vector2(cell.x * 25, cell.y * 15);
    }

}
