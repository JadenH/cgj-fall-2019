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
    public HashSet<Cell> Taken = new HashSet<Cell>();

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
            // TODO: This is where we tweak our generation.
            // TODO: Could make it less likely to pick rooms closer to middle? Etc...
            var validRooms = Rooms.Where(room => room.HasAvailableDoor()).ToArray();
            var randomRoom = validRooms[Random.Range(0, validRooms.Length)];
            var validDoors = randomRoom.UnusedDoors().ToArray();
            var randomDoor = validDoors[Random.Range(0, validDoors.Length)];

            // Create new room
            var neighborCell = GetNeighborCell(randomRoom.Cell, randomDoor.Direction);

            // Check if that cell already has a room
            if (!Taken.Contains(neighborCell))
            {
                var newRoom = CreateRoom(neighborCell);
                var connectingDoor = newRoom.GetDoorForDirection(randomDoor.Direction.Opposite());

                // Connect up our random picked room to the new one
                randomRoom.SetNeighborRoom(randomDoor.Direction, newRoom);
                randomDoor.MarkAsUsed();
                randomDoor.ConnectingRoom = newRoom;

                // Connect up our new room to our random picked room
                newRoom.SetNeighborRoom(randomDoor.Direction.Opposite(), randomRoom);
                connectingDoor.MarkAsUsed();
                connectingDoor.ConnectingRoom = randomRoom;

                Taken.Add(neighborCell);
            }
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
        //room.LockAllDoors();
        Rooms.Add(room);
        UpdateAllDoors(cell);
        // room.MarkDoorAsUsed(DoorToMark);
        return room;
    }

    private void UpdateAllDoors(Room room)
    {
        Cell Currentcell = room.Cell;
        Cell NeighborCellUp = GetNeighborCell(Currentcell, Direction.Up);
        // check if cell has room
        //if yes mark neignbor door down as used and current door up as used.
    }

    private void LockDoors(Room room)
    {

    }

    // Update is called once per frame
    private void Update()
    {
    }
}