using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Chaser : Mover
{
    public float Speed = .01f;
    public float RepathDelay = 0.05f;

    private Queue<Vector2Int> _path;
    private Vector2Int? _next;

    private void Start()
    {
        StartCoroutine(PathLoop());
    }

    private void Update()
    {
        if (_next.HasValue)
        {
            if (Vector2.Distance(transform.position, (Vector2)_next) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, (Vector2)_next, Speed);
            }
            else
            {
                _next = _path.Any() ? _path.Dequeue() : (Vector2Int?)null;
            }
        }
        else if (_path != null && _path.Any())
        {
            _next = _path.Dequeue();
        }
    }

    private IEnumerator PathLoop()
    {
        while (enabled)
        {
            var start = _next ?? new Vector2Int((int) transform.position.x, (int) transform.position.y);
            var target = new Vector2Int((int) Player.Character.transform.position.x, (int) Player.Character.transform.position.y);
            var path = Pathfind(start, target);
            if (path != null && path.Any())
            {
                _path = new Queue<Vector2Int>(path);
            }

            yield return new WaitForSeconds(RepathDelay);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        if (_path != null)
        {
            Gizmos.color = Color.yellow;
            foreach (var cell in _path)
            {
                Gizmos.DrawSphere(cell + Vector2.one * .5f, 0.25f);
            }
        }
    }
}
