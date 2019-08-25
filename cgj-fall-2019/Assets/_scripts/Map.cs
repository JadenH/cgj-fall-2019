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
        Door top_door = tempRoom.transform.GetChild(0).gameObject.GetComponent<Door>();
        Door right_door = tempRoom.transform.GetChild(1).gameObject.GetComponent<Door>();
        Door bottom_door = tempRoom.transform.GetChild(2).gameObject.GetComponent<Door>();
        Door left_door = tempRoom.transform.GetChild(3).gameObject.GetComponent<Door>();
    }

    void CreateMap(int rooms)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
