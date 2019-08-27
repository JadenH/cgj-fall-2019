using System;
using UnityEngine;

[Serializable]
public struct Cell
{
    public int X;
    public int Y;

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static explicit operator Vector2(Cell cell)
    {
        return new Vector2(cell.X * 25, cell.Y * 15);
    }

    public static explicit operator Cell(Vector2 v2)
    {
        return new Cell(Mathf.FloorToInt(v2.x / Game.CellSize), Mathf.FloorToInt(v2.y / Game.CellSize));
    }

    public static Cell operator+ (Cell c1, Cell c2)
    {
        return new Cell(c1.X + c2.X, c1.Y + c2.Y);
    }

    public static Cell operator -(Cell c1, Cell c2)
    {
        return new Cell(c1.X - c2.X, c1.Y - c2.Y);
    }

    public static Cell zero => new Cell(0,0);
    public static Cell up => new Cell(0,1);
    public static Cell down => new Cell(0, -1);
    public static Cell right => new Cell(1,0);
    public static Cell left => new Cell(-1,0);
}