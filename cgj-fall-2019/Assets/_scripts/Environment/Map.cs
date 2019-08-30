using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : GameBehaviour
{
    public Room RoomPrefab;

    private Dictionary<Vector2Int, Room> _mapRoomCell = new Dictionary<Vector2Int, Room>();
    private Dictionary<Vector2Int, Room> _mapWorldCell = new Dictionary<Vector2Int, Room>();

    private Room[] _rooms => _mapRoomCell.Values.ToArray();

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
        var startRoom = CreateRoom(Vector2Int.zero);
        CameraTarget.position = startRoom.transform.position + new Vector3(0.5f, 0.5f);

        while (numberOfRooms > _mapRoomCell.Count)
        {
            // Find a valid room and door to extend off from
            // TODO: This is where we tweak our generation.
            // TODO: Could make it less likely to pick rooms closer to middle? Etc...
            var validRooms = _rooms.Where(room => room.HasAvailableDoor()).ToArray();
            var randomRoom = validRooms[Random.Range(0, validRooms.Length)];
            var validDoors = randomRoom.UnusedDoors().ToArray();
            var randomDoor = validDoors[Random.Range(0, validDoors.Length)];

            // Create new room
            var neighborRoomCell = randomRoom.RoomCell.GetNextRoomCell(randomDoor.Direction);

            // Check if that roomCell already has a room
            if (!_mapRoomCell.ContainsKey(neighborRoomCell))
            {
                var newRoom = CreateRoom(neighborRoomCell);

                ConnectDoors(newRoom);
                ConnectDoors(randomRoom);

                newRoom.UnlockAllDoors();
                randomRoom.UnlockAllDoors();
            }
        }
    }

    private Room CreateRoom(Vector2Int roomCell)
    {
        var room = Instantiate(RoomPrefab, roomCell.RoomPosition(), Quaternion.identity).GetComponent<Room>();

        _mapRoomCell.Add(roomCell, room);
        room.Map = this;
        room.RoomCell = roomCell;

        foreach (var cell in room.GetCells())
        {
            var world = room.RenderTilemap.CellToWorld(cell);
            var worldCell = new Vector2Int((int)world.x, (int)world.y);
            _mapWorldCell.Add(worldCell, room);
        }

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
                var neighborDoor = neighborRoom.GetDoorForDirection(direction.Opposite());

                if (!door.Used)
                {
                    door.ConnectingDoor = neighborDoor;
                    door.ConnectingRoom = neighborRoom;
                    door.Used = true;
                    door.LockDoor();
                }

                if (!neighborDoor.Used)
                {
                    neighborDoor.ConnectingDoor = door;
                    neighborDoor.ConnectingRoom = room;
                    neighborDoor.Used = true;
                    neighborDoor.LockDoor();
                }
            }
        }
    }

    public Room GetRoomForRoomCell(Vector2Int roomCell)
    {
        return _mapRoomCell.ContainsKey(roomCell) ? _mapRoomCell[roomCell] : null;
    }

    public Room GetRoomForCell(Vector2Int cell)
    {
        if (!_mapWorldCell.ContainsKey(cell)) return null;
        return _mapWorldCell[cell];
    }

    public bool IsPathable(Vector2Int worldCell)
    {
        var room = GetRoomForCell(worldCell);
        if (!room) return false;
        return room.IsPathable(worldCell);
    }
}