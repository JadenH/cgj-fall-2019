using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : GameBehaviour
{
    public Room RoomPrefab;

    private Dictionary<Cell, Room> _map = new Dictionary<Cell, Room>();

    private Room[] _rooms => _map.Values.ToArray();

    public void Generate(int level)
    {
        // Destroy

        // Number of rooms is equal to level
        CreateMap(level);
        CreatePortals();
    }

    private void CreatePortals()
    {
        var randomRoom = _rooms[Random.Range(0, _rooms.Length)];
        randomRoom.CreatePortals();
    }

    public void CreateMap(int numberOfRooms)
    {
        var startRoom = CreateRoom(Cell.zero);
        CameraTarget.position = startRoom.transform.position + new Vector3(0.5f, 0.5f);

        while (numberOfRooms > _map.Count)
        {
            // Find a valid room and door to extend off from
            // TODO: This is where we tweak our generation.
            // TODO: Could make it less likely to pick rooms closer to middle? Etc...
            var validRooms = _rooms.Where(room => room.HasAvailableDoor()).ToArray();
            var randomRoom = validRooms[Random.Range(0, validRooms.Length)];
            var validDoors = randomRoom.UnusedDoors().ToArray();
            var randomDoor = validDoors[Random.Range(0, validDoors.Length)];

            // Create new room
            var neighborCell = randomRoom.Cell.GetNext(randomDoor.Direction);

            // Check if that cell already has a room
            if (!_map.ContainsKey(neighborCell))
            {
                var newRoom = CreateRoom(neighborCell);

                ConnectDoors(newRoom);
                ConnectDoors(randomRoom);
            }
        }
    }

    private Room CreateRoom(Cell cell)
    {
        var room = Instantiate(RoomPrefab, cell.RoomPosition(), Quaternion.identity).GetComponent<Room>();

        _map.Add(cell, room);
        room.Map = this;
        room.Cell = cell;

        //room.LockAllDoors();
        // room.MarkDoorAsUsed(DoorToMark);
        return room;
    }

    private void ConnectDoors(Room room)
    {
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var neighborRoom = room.GetNeighborRoom(direction);
            if (neighborRoom)
            {
                var door = room.GetDoorForDirection(direction);
                if (!door.Used)
                {
                    door.ConnectingRoom = neighborRoom;
                    door.Used = true;
                    door.LockDoor();
                }

                var neighborDoor = neighborRoom.GetDoorForDirection(direction.Opposite());
                if (!neighborDoor.Used)
                {
                    neighborDoor.ConnectingRoom = room;
                    neighborDoor.Used = true;
                    neighborDoor.LockDoor();
                }
            }
        }
    }

    private void LockDoors(Room room)
    {

    }

    // Update is called once per frame
    private void Update()
    {
    }

    public Room GetRoomAtCell(Cell cell)
    {
        return _map.ContainsKey(cell) ? _map[cell] : null;
    }
}