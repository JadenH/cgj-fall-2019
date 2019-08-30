using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Chaser : Mover
{
    public Transform Target;

    private void OnEnable()
    {
        StartCoroutine(PathLoop());
    }

    private IEnumerator PathLoop()
    {
        while (enabled)
        {
            if (Target)
            {
                var target = new Vector2Int((int)Target.position.x, (int)Target.position.y);
                Move(target);
            }

            yield return new WaitForSeconds(RepathDelay);
        }
    }

}
