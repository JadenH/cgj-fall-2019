using System;
using UnityEngine;
using System.Collections;
//using UnityEditorInternal;

[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Health))]
public class Skeleton : Enemy
{
    private Chaser _chaser;
    private Mover _mover;
    private Health _health;

    private State _currentState;

    private void Awake()
    {
        _chaser = GetComponent<Chaser>();
        _mover = GetComponent<Mover>();
        _health = GetComponent<Health>();

        _currentState = State.Attack;
    }

    protected override void PlayerChangedRooms(Room room)
    {
        if (room != CurrentRoom)
        {
            StopAllCoroutines();
            _chaser.enabled = false;
            _mover.Move(StartCell);
        }
        else
        {
            _currentState = State.Attack;
            StartCoroutine(RunAi());
        }
    }

    private enum State
    {
        Heal,
        Attack,
        Idle
    }

    private IEnumerator RunAi()
    {
        while (enabled)
        {
            switch (_currentState)
            {
                case State.Heal:
                    _chaser.enabled = false;

                    while (_health.CurrentHealth < _health.MaxHealth)
                    {
                        _health.AddHealth(25f);
                        yield return new WaitForSeconds(0.25f);
                    }

                    _currentState = State.Attack;
                    break;
                case State.Attack:
                    _chaser.enabled = true;
                    _chaser.Target = Player.transform;

                    while (true)
                    {
                        if (Player.CurrentRoom != CurrentRoom)
                        {
                            break;
                        }

                        if (_health.CurrentHealth / _health.MaxHealth <= .25)
                        {
                            _currentState = State.Heal;
                            break;
                        }
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            yield return null;
        }
    }
}