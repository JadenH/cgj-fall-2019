using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mover : Pathfinder
{
    public float Speed = .01f;

    public Queue<Vector2Int> CurrentPath { get; private set; }
    public Vector2Int? Next { get; private set; }

    public void Move(Vector2Int dest)
    {
        var start = Next ?? Map.GetCellForPosition(transform.position);
        var path = Pathfind(start, dest);
        if (path != null && path.Any())
        {
            CurrentPath = new Queue<Vector2Int>(path);
        }
    }

    private void Update()
    {
        if (Next.HasValue)
        {
            var targetPos = Map.GetCellCenter(Next.Value);
            if (Vector2.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * Speed);
            }
            else
            {
                Next = CurrentPath.Any() ? CurrentPath.Dequeue() : (Vector2Int?)null;
            }
        }
        else if (CurrentPath != null && CurrentPath.Any())
        {
            Next = CurrentPath.Dequeue();
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        if (CurrentPath != null)
        {
            Gizmos.color = Color.yellow;
            foreach (var cell in CurrentPath)
            {
                Gizmos.DrawSphere(cell + Vector2.one * .5f, 0.25f);
            }
        }
    }
}
