using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    public int number_of_rooms = 3;
    public GameObject room_prefab;

    List<GameObject> exits = new List<GameObject>();

    public List<Room> rooms = new List<Room>();




    // Start is called before the first frame update
    void Start()
    {
        CreateMap(number_of_rooms);
        GameObject tempRoom = Instantiate(room_prefab, new Vector3(0, 0, 0), Quaternion.identity);
        Room room = tempRoom.GetComponent<Room>();
        room.Lock_top_door();
        room.Lock_right_door();
        room.Lock_bottom_door();
        room.Lock_left_door();
        rooms.Add(room);
    }

    void CreateMap(int rooms)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
