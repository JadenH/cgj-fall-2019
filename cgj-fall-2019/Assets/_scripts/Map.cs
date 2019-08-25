using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    public int rooms = 3;
    public GameObject room_prefab;

    List<GameObject> exits = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        CreateMap(rooms);
        GameObject tempRoom = Instantiate(room_prefab, new Vector3(0, 0, 0), Quaternion.identity);
        Room room = tempRoom.GetComponent<Room>();
        room.Lock_top_door();
    }

    void CreateMap(int rooms)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
