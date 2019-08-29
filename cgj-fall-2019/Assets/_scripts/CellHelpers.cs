using System;
using UnityEngine;
using System.Collections;

public static class CellHelpers
{
    public static Cell GetNext(this Cell cell, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return cell + Cell.up;
            case Direction.Right:
                return cell + Cell.right;
            case Direction.Down:
                return cell + Cell.down;
            case Direction.Left:
                return cell + Cell.left;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    public static Vector2 RoomPosition(this Cell cell)
    {
        return new Vector2(cell.X * 25, cell.Y * 15);
    }
}
