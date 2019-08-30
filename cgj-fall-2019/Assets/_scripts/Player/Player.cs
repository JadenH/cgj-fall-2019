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
        Debug.Log("Change room", room);
        CurrentRoom = room;
        room.PlayerEnter();
        ChangedRoom?.Invoke(room);
    }
}