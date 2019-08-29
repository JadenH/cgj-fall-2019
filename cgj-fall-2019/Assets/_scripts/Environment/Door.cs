using UnityEngine;

public class Door : GameBehaviour
{
    public Direction Direction;
    public bool Locked = true;
    public bool Used = false;
    public bool Lie = false;

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

    public void MarkAsLie()
    {
        Lie = true;
    }

    public bool IsLocked()
    {
        return Locked;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>())
        {
            if (ConnectingRoom)
            {
                CameraTarget.transform.position = ConnectingRoom.transform.position;
                switch (Direction)
                {
                    case Direction.Up:
                        Player.transform.position = ConnectingRoom.transform.position - new Vector3(0, +5, 0);
                        break;
                    case Direction.Right:
                        Player.transform.position = ConnectingRoom.transform.position - new Vector3(+10, 0, 0);
                        break;
                    case Direction.Down:
                        Player.transform.position = ConnectingRoom.transform.position - new Vector3(0, -5, 0);
                        break;
                    case Direction.Left:
                        Player.transform.position = ConnectingRoom.transform.position - new Vector3(-10, 0, 0);
                        break;
                }

                Player.EnterRoom(ConnectingRoom);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}