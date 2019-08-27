using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private List<GameObject> _exits = new List<GameObject>();

    public int NumberOfRooms = 3;
    public GameObject RoomPrefab;

    public List<Room> rooms = new List<Room>();

    public Room[,] RoomList = new Room[30, 30];




    // Start is called before the first frame update
    private void Start()
    {
        CreateMap(NumberOfRooms);
    }

    private void CreateMap(int NumberOfRooms)
    {
        CreateRoom(15, 15, Direction.Down);
        NumberOfRooms--;
        while (NumberOfRooms > 0)
        { 
            bool KeepSearching = true;
            bool IsUsed = false;
            while(KeepSearching)
            {

                int RandRoom = Random.Range(0, rooms.Count);
                int RandDoor = Random.Range(0, 4);
                Debug.Log(RandDoor);
                Debug.Log(RandRoom);
                IsUsed = IsLocationUsed(RandRoom, RandDoor);
                
                if (!IsUsed)
                {
                    KeepSearching = false;
                    Room TempRoom = rooms[RandRoom];
                    if (RandDoor == 0)
                    {
                        Debug.Log("up");
                        //up
                       CreateRoom(TempRoom.LocationX, TempRoom.LocationY + 1, Direction.Down);
                        TempRoom.MarkDoorAsUsed(Direction.Up);
                    }
                    else if (RandDoor == 1)
                    {
                        //Right
                        Debug.Log("right");
                        CreateRoom(TempRoom.LocationX + 1, TempRoom.LocationY, Direction.Left);
                        TempRoom.MarkDoorAsUsed(Direction.Right);
                    }
                    else if (RandDoor == 2)
                    {
                        //down
                        Debug.Log("down");
                        CreateRoom(TempRoom.LocationX, TempRoom.LocationY -1, Direction.Up);
                        TempRoom.MarkDoorAsUsed(Direction.Down);
                    }
                    else if (RandDoor == 3)
                    {
                        //Left
                        Debug.Log("left");
                        CreateRoom(TempRoom.LocationX -1, TempRoom.LocationY, Direction.Right);
                        TempRoom.MarkDoorAsUsed(Direction.Left);
                    }
                    NumberOfRooms--;

                }
            }
        }
    }

    bool IsLocationUsed(int placeLocation, int RandDoor)
    {
        bool Result = true;
        Room TempRoom = rooms[placeLocation];
        
        if(RandDoor == 0)
        {
            Result = TempRoom.IsDoorUsed(Direction.Up);
        }else if(RandDoor == 1)
        {
            Result = TempRoom.IsDoorUsed(Direction.Right);
        }
        else if (RandDoor == 2)
        {
            Result =TempRoom.IsDoorUsed(Direction.Down);
        }
        else if (RandDoor == 3)
        {
            Result = TempRoom.IsDoorUsed(Direction.Left);

        }
        return Result;
    }


    void CreateRoom(int x, int y, Direction DoorToMark)
    {
        
        var tempRoom = Instantiate(RoomPrefab, new Vector3(x * 25, y * 14, 0), Quaternion.identity);
        var room = tempRoom.GetComponent<Room>();
        room.LocationX = x;
        room.LocationY = y;
       // room.MarkDoorAsUsed(DoorToMark);
        Debug.Log("door getting locked: " + DoorToMark);
       // LockDoors(room);
        RoomList[x, y] = room;
        rooms.Add(room);
        UpdateUsedDoors(x, y);


    }

    void UpdateUsedDoors(int x, int y)
    {
        if (RoomList[x, y + 1] == null && RoomList[x, y - 1] == null && RoomList[x + 1, y] == null && RoomList[x - 1, y] == null)
        {
         //   RoomList[x, y].MarkDoorAsUsed(Direction.Down);
        }

        //up
        if (RoomList[x, y + 1] != null )
        {
            Debug.Log("X Up");

            RoomList[x , y + 1].MarkDoorAsUsed(Direction.Down);
            RoomList[x, y].MarkDoorAsUsed(Direction.Up);
        }

        //down
        if (RoomList[x , y - 1] != null)
        {
            Debug.Log("X Up");

            RoomList[x , y - 1].MarkDoorAsUsed(Direction.Up);
            RoomList[x, y].MarkDoorAsUsed(Direction.Down);

        }

        //right
        if (RoomList[x + 1, y] != null)
        {
            Debug.Log("X Up");

            RoomList[x + 1, y].MarkDoorAsUsed(Direction.Left);
            RoomList[x, y].MarkDoorAsUsed(Direction.Right);

        }

        //left
        if (RoomList[x - 1, y] != null)
        {
            Debug.Log("X Up");

            RoomList[x - 1, y].MarkDoorAsUsed(Direction.Right);
            RoomList[x, y].MarkDoorAsUsed(Direction.Left);

        }
    }

    void LockDoors(Room room)
    {
        room.LockDoor(Direction.Up);
        room.LockDoor(Direction.Down);
        room.LockDoor(Direction.Right);
        room.LockDoor(Direction.Left);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}