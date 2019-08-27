using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : GameBehaviour
{
    private List<GameObject> _exits = new List<GameObject>();

    public int NumberOfRooms = 3;
    public Room RoomPrefab;

    public List<Room> Rooms = new List<Room>();

    // Start is called before the first frame update
    private void Start()
    {
        CreateMap(NumberOfRooms);
    }

    private void CreateMap(int numberOfRooms)
    {
        var startRoom = CreateRoom(Cell.zero);
        CameraTarget.position = startRoom.transform.position;
        while (numberOfRooms > Rooms.Count)
        {
            // Find a valid room and door to extend off from
            var validRooms = Rooms.Where(room => room.HasAvailableDoor()).ToArray();
            var randomRoom = validRooms[Random.Range(0, validRooms.Length)];
            var validDoors = randomRoom.UnusedDoors().ToArray();
            var randomDoor = validDoors[Random.Range(0, validDoors.Length)];

            // Create new room
            var neighborCell = GetNeighborCell(randomRoom.Cell, randomDoor.Direction);
            var newRoom = CreateRoom(neighborCell);
            var connectingDoor = newRoom.GetDoorForDirection(randomDoor.Direction.Opposite());

            randomRoom.SetNeighborRoom(randomDoor.Direction, newRoom);
            randomDoor.MarkAsUsed();
            randomDoor.ConnectingRoom = newRoom;

            newRoom.SetNeighborRoom(randomDoor.Direction.Opposite(), randomRoom);
            connectingDoor.MarkAsUsed();
            connectingDoor.ConnectingRoom = randomRoom;
        }
    }

    private Cell GetNeighborCell(Cell randomRoomCell, Direction randomDoorDirection)
    {
        switch (randomDoorDirection)
        {
            case Direction.Up:
                return randomRoomCell + Cell.up;
            case Direction.Right:
                return randomRoomCell + Cell.right;
            case Direction.Down:
                return randomRoomCell + Cell.down;
            case Direction.Left:
                return randomRoomCell + Cell.left;
            default:
                throw new ArgumentOutOfRangeException(nameof(randomDoorDirection), randomDoorDirection, null);
        }
    }


    private Room CreateRoom(Cell cell)
    {
        var room = Instantiate(RoomPrefab, (Vector2)cell, Quaternion.identity).GetComponent<Room>();
        room.Cell = cell;
        room.LockAllDoors();
        Rooms.Add(room);

        // room.MarkDoorAsUsed(DoorToMark);
        return room;
    }

    private void LockDoors(Room room)
    {

    }

    // Update is called once per frame
    private void Update()
    {
    }
}