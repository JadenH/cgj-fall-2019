﻿using UnityEngine;
using System.Collections;

public abstract class GameBehaviour : MonoBehaviour
{
    public Transform CameraTarget => Game.Instance.CameraTarget;
    public Player Player => Game.Instance.Player;
}