using UnityEngine;

public class Door : GameBehaviour
{
    public Direction Direction;
    public bool Locked = true;
    public bool Used = false;

    public Room ConnectingRoom;

    public BoxCollider2D Collide;

    public GameObject LockedDoorSprite;

    public float PlayerX = 0;
    public float PlayerY = 0;
    public GameObject UnlockedDoorSprite;

    // Start is called before the first frame update
    private void Start()
    {
    }

    public void LockDoor()
    {
        LockedDoorSprite.SetActive(true);
        UnlockedDoorSprite.SetActive(false);
        Collide.isTrigger = false;
        Locked = true;
    }

    public void UnlockDoor()
    {
        LockedDoorSprite.SetActive(false);
        UnlockedDoorSprite.SetActive(true);
        Collide.isTrigger = true;
        Locked = false;
    }

    public bool IsLocked()
    {
        return Locked;
    }

    public bool IsUsed()
    {
        return Used;
    }

    public void MarkAsUsed()
    {
        Used = true;
        UnlockDoor();
    }

    private void OnTriggerEnter2D(Collider2D theCollision)
    {
        if (theCollision.tag == "Player")
        {
            Room room = GetComponentInParent<Room>();
            CameraTarget.transform.position = ConnectingRoom.transform.position;
            Player.transform.position = ConnectingRoom.transform.position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}