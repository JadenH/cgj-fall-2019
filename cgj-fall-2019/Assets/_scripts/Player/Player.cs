using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class Player : GameBehaviour
{
    public Character Character;
    public Room CurrentRoom;
    public PlayerLife Life;
    public Health Health;

    [Serializable]
    public class ChangeRoomEvent : UnityEvent<Room> { }

    public ChangeRoomEvent ChangedRoom;

    private void Start()
    {
        Health.HealthChanged.AddListener(HandleHealthChanged);
    }

    private void HandleHealthChanged(float current, float delta, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Bomb:
                CameraController.Shake();
                break;
            case DamageType.Gun:
                break;
            case DamageType.Bite:
                CameraController.Shake(0.1f, 0.1f);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(damageType), damageType, null);
        }
    }

    public void EnterRoom(Room room)
    {
        CurrentRoom = room;
        room.PlayerEntered();
        ChangedRoom?.Invoke(room);
    }
}