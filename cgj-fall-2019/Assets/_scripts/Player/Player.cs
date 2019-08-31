using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public Character Character;
    public Room CurrentRoom;
    public PlayerLife Life;
    public Health Health;

    [Serializable]
    public class ChangeRoomEvent : UnityEvent<Room> { }

    public ChangeRoomEvent ChangedRoom;

    public void EnterRoom(Room room)
    {
        CurrentRoom = room;
        ChangedRoom?.Invoke(room);
    }
}