using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mover : GameBehaviour
{
    public Room Room;
    public float Speed;

    private Cell[] _path;
    private Coroutine _pathingCoroutine;

    private void Start()
    {
        StartCoroutine(PathLoop());
    }

    private IEnumerator PathLoop()
    {
        while (enabled)
        {
            if (Room && Room == Player.CurrentRoom)
            {
                var path = Pathfind((Cell)transform.position, (Cell)Player.Character.transform.position);
                if (path != null && path.Any())
                {
                    _path = path;
                    if (_pathingCoroutine != null) StopCoroutine(_pathingCoroutine);
                    _pathingCoroutine = StartCoroutine(Move(new Queue<Cell>(path)));
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator Move(Queue<Cell> path)
    {
        var dest = path.Last();
        var current = path.Dequeue();
        while (Vector2.Distance(transform.position, (Vector2)dest) > 0.1f)
        {
            if (Vector2.Distance(transform.position, (Vector2)current) <= 0.1f)
            {
                current = path.Dequeue();
            }
            transform.position = Vector3.MoveTowards(transform.position, (Vector2)current, Speed);
            yield return new WaitForEndOfFrame();
        }
    }

    private struct CostCell
    {
        public Cell Cell;
        public int Cost;

        public CostCell(Cell cell, int cost = 0)
        {
            Cell = cell;
            Cost = cost;
        }
    }

    // A* from https://www.redblobgames.com/pathfinding/a-star/introduction.html
    public Cell[] Pathfind(Cell start, Cell dest)
    {
        if (!Pathable(start, dest)) return null;

        var frontier = new HashSet<CostCell>();
        var cameFrom = new Dictionary<Cell, Cell?>();
        var costSoFar = new Dictionary<Cell, int> { [start] = 0 };

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

        var result = new List<Cell> { dest };
        var currentCell = cameFrom[dest];
        while (currentCell.HasValue)
        {
            result.Add(currentCell.Value);
            currentCell = cameFrom[currentCell.Value];
        }

        result.Reverse();
        return result.ToArray();
    }

    private int Heuristic(Cell current, Cell next)
    {
        return Mathf.FloorToInt(Vector2.Distance((Vector2)current, (Vector2)next));
    }

    private Cell[] GetNeighbors(Cell cell)
    {
        return new[]
        {
            cell + Cell.up,
            cell + Cell.right,
            cell + Cell.down,
            cell + Cell.left
        };
    }

    private bool Pathable(Cell start, Cell dest)
    {
        if (!Room.Pathable.ContainsKey(start))
        {
            Debug.LogError("Start is not in room!");
            return false;
        }

        if (!Room.Pathable.ContainsKey(dest))
        {
            Debug.LogError("Destination is not in room!");
            return false;
        }

        if (!Room.Pathable[dest])
        {
            Debug.LogError("Destination is not pathable!");
            return false;
        }

        if (!Room.Pathable[start])
        {
            Debug.LogError("Start is not pathable!");
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        if (_path != null)
        {
            Gizmos.color = Color.yellow;
            foreach (var cell in _path)
            {
                Gizmos.DrawSphere((Vector2)cell, 0.25f);
            }
        }
    }
}
