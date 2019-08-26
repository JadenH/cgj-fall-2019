using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private List<GameObject> _exits = new List<GameObject>();

    public int NumberOfRooms = 3;
    public GameObject RoomPrefab;

    public List<Room> rooms = new List<Room>();

    List<List<Room>> grid = new List<List<Room>>();
    public Room[,] RoomList = new Room[10, 10];




    // Start is called before the first frame update
    private void Start()
    {
        CreateMap(NumberOfRooms);
       
    }

    private void CreateMap(int NumberOfRooms)
    {
        //CreateRoom(0, 0);
        NumberOfRooms--;
        while(NumberOfRooms > 0)
        {
            int side = Random.Range(0, 4);
            
            
            NumberOfRooms--;
        }
       // CreateRoom(0, 0);
       // CreateRoom(0, 1);

    }


    void CreateRoom(int x, int y)
    {
        
        var tempRoom = Instantiate(RoomPrefab, new Vector3(x * 25, y * 14, 0), Quaternion.identity);
        var room = tempRoom.GetComponent<Room>();
        LockDoors(room);
        RoomList[x, y] = room;
      //  Debug.Log(RoomList[x,y]);
      //  Debug.Log(RoomList[1, 1]);

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