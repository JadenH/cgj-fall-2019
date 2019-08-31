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

    public bool SentLifeData = false;

    [Serializable]
    public class ChangeRoomEvent : UnityEvent<Room> { }

    public ChangeRoomEvent ChangedRoom;
    public float DamageModifier;

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
            case DamageType.Heal:
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

    public void ResetHealth()
    {
        Health.CurrentHealth = Health.MaxHealth;
        Player.Health.TakeDamage(0, DamageType.Bite);
        Health.HealthChanged.AddListener(HandleHealthChanged);
    }

    private void Update()
    {
        if(!SentLifeData)
        {
            if (Health.CurrentHealth <= 0)
            {
                SentLifeData = true;
                PlayerLife.CurrentLives--;
                Game.CurrentLevelNumber = Mathf.Max(1, Game.CurrentLevelNumber - 1);
                Game.StartLevel(Game.CurrentLevelNumber);
                Game._answer = "Lost a life! Back to Level " + Game.CurrentLevelNumber;
                Game.StartCoroutine("WaitAndPrint");
                ResetHealth();
            }
        }

    }
}