using System;
using UnityEngine;
using System.Collections;

public static class DirectionUtil 
{
    public static Direction Opposite(this Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Right:
                return Direction.Left;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
}
