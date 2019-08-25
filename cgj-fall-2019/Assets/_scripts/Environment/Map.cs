using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private List<GameObject> _exits = new List<GameObject>();

    public int NumberOfRooms = 3;
    public GameObject RoomPrefab;

    public List<Room> rooms = new List<Room>();

    // Start is called before the first frame update
    private void Start()
    {
        CreateMap(NumberOfRooms);
        var tempRoom = Instantiate(RoomPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        var room = tempRoom.GetComponent<Room>();
        room.LockDoor(Direction.Up);
        room.LockDoor(Direction.Down);
        room.LockDoor(Direction.Right);
        room.LockDoor(Direction.Left);
        rooms.Add(room);
    }

    private void CreateMap(int rooms)
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}