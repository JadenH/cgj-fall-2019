using UnityEngine;

public class Door : GameBehaviour
{
    public Direction Direction;
    public bool Locked = true;
    public bool Used = false;

    public Room ConnectingRoom;
    public Door ConnectingDoor;

    public BoxCollider2D Collide;

    public GameObject LockedDoorSprite;
    public GameObject UnlockedDoorSprite;
    public GameObject SealedDoorSprite;

    public void LockDoor()
    {
        LockedDoorSprite.SetActive(true);
        UnlockedDoorSprite.SetActive(false);
        SealedDoorSprite.SetActive(false);

        Collide.isTrigger = false;
        Locked = true;
    }

    public void UnlockDoor()
    {
        LockedDoorSprite.SetActive(false);
        UnlockedDoorSprite.SetActive(true);
//        SealedDoorSprite.SetActive(false);

        Collide.isTrigger = true;
        Locked = false;
    }

    public bool IsLocked()
    {
        return Locked;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>())
        {
            if (ConnectingRoom && !Locked)
            {
                CameraTarget.transform.position = ConnectingRoom.transform.position + new Vector3(0.5f, 0.5f);
                Player.transform.position = ConnectingDoor.transform.position + Direction.V3() - (Vector3) Player.GetComponent<Collider2D>().offset;
                Player.EnterRoom(ConnectingRoom);
            }
        }
    }
}