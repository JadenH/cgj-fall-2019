using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Chaser))]
public class Bat : Enemy
{
    public Chaser Chaser;

    protected override void PlayerChangedRooms(Room room)
    {
        Debug.Log("HERE");
        if (room == CurrentRoom)
        {
            Chaser.enabled = true;
            Chaser.Target = Player.transform;
        }
        else
        {
            Chaser.enabled = false;
            Chaser.Move(StartCell);
        }
    }
}
