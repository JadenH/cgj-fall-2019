using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Mover))]
public class Chaser : GameBehaviour
{
    public Transform Target;
    public float RepathDelay = 0.05f;

    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

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
                _mover.Move(Map.GetCellForPosition(Target.position));
            }

            yield return new WaitForSeconds(RepathDelay);
        }
    }

}
