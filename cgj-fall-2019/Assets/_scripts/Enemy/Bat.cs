﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Mover))]
public class Bat : Enemy
{
    private Chaser _chaser;
    private Mover _mover;

    private void Awake()
    {
        _chaser = GetComponent<Chaser>();
        _mover = GetComponent<Mover>();
    }

    protected override void PlayerChangedRooms(Room room)
    {
        if (room == CurrentRoom)
        {
            _chaser.enabled = true;
            _chaser.Target = Player.transform;
        }
        else
        {
            _chaser.enabled = false;
            _mover.Move(StartCell);
        }
    }
}
