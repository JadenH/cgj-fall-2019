﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mover : GameBehaviour
{
    private struct CostCell
    {
        public Vector2Int Cell;
        public int Cost;

        public CostCell(Vector2Int cell, int cost = 0)
        {
            Cell = cell;
            Cost = cost;
        }
    }

    // A* from https://www.redblobgames.com/pathfinding/a-star/introduction.html
    public Vector2Int[] Pathfind(Vector2Int start, Vector2Int dest)
    {
        if (!Pathable(start, dest)) return null;

        var frontier = new HashSet<CostCell>();
        var cameFrom = new Dictionary<Vector2Int, Vector2Int?>();
        var costSoFar = new Dictionary<Vector2Int, int> { [start] = 0 };

        frontier.Add(new CostCell(start));
        cameFrom.Add(start, null);

        while (frontier.Any())
        {
            var current = frontier.OrderBy(cell => cell.Cost).First();
            frontier.Remove(current);

            if (current.Cell == dest) break;

            var neighbors = GetNeighbors(current.Cell);
            foreach (var next in neighbors)
            {
                if (!Map.IsPathable(next)) continue;
                var newCost = costSoFar[current.Cell] + 1;

                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    var priority = newCost + Heuristic(current.Cell, next);
                    frontier.Add(new CostCell(next, priority));

                    if (cameFrom.ContainsKey(next)) cameFrom[next] = current.Cell;
                    else cameFrom.Add(next, current.Cell);
                }
            }
        }

        if (!cameFrom.ContainsKey(dest)) return null;

        var result = new List<Vector2Int> { dest };
        var currentCell = cameFrom[dest];
        while (currentCell.HasValue)
        {
            result.Add(currentCell.Value);
            currentCell = cameFrom[currentCell.Value];
        }

        result.Reverse();
        return result.ToArray();
    }

    private int Heuristic(Vector2Int current, Vector2Int next)
    {
        return Mathf.FloorToInt(Vector2.Distance((Vector2)current, (Vector2)next));
    }

    private Vector2Int[] GetNeighbors(Vector2Int cell)
    {
        return new[]
        {
            cell + Vector2Int.up,
            cell + Vector2Int.right,
            cell + Vector2Int.down,
            cell + Vector2Int.left
        };
    }

    private bool Pathable(Vector2Int start, Vector2Int dest)
    {
        if (!Map.IsPathable(start))
        {
            Debug.LogError("Start is not pathable!");
            return false;
        }

        if (!Map.IsPathable(dest))
        {
            Debug.LogError("Destination is not pathable!");
            return false;
        }

        return true;
    }
}
