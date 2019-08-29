using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Mover : MonoBehaviour
{
    public Room room;

    private class CostCell
    {
        public Cell Cell;
        public int Cost;

        public CostCell(Cell cell, int cost)
        {
            Cell = cell;
            Cost = cost;
        }
    }

    public void Pathfind(Cell start, Cell dest)
    {
        if (!Pathable(start, dest)) return;

        var open = new HashSet<CostCell>();
        var closed = new HashSet<CostCell>();

        open.Add(new CostCell(start, 0));

        while (open.Any())
        {
            var current = open.OrderBy(cell => cell.Cost).First();
            open.Remove(current);
            closed.Add(current);

            if (current.Cell == dest)
            {
                return;
            }

//            var adjacent = GetAdjacentCells(current.Cell);
        }
    }

//    private Cell[] GetAdjacentCells(Cell cell)
//    {
//        return new []
//        {
//
//        }
//    }

    private bool Pathable(Cell start, Cell dest)
    {
        if (!room.Pathable.ContainsKey(start))
        {
            Debug.LogError("Start is not in room!");
            return false;
        }

        if (!room.Pathable.ContainsKey(dest))
        {
            Debug.LogError("Destination is not in room!");
            return false;
        }

        if (!room.Pathable[dest])
        {
            Debug.LogError("Destination is not pathable!");
            return false;
        }

        if (!room.Pathable[start])
        {
            Debug.LogError("Start is not pathable!");
            return false;
        }

        return true;
    }
}
