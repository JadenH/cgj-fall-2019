using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public Character Character;
    public Room CurrentRoom;
    public PlayerHealth Health;

    public void EnterRoom(Room room)
    {
        CurrentRoom = room;
    }
}