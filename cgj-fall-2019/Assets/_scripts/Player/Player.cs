using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public Character Character;
    public Room CurrentRoom;
    public PlayerLife Life;
    public Health Health;

    public void EnterRoom(Room room)
    {
        CurrentRoom = room;
    }
}