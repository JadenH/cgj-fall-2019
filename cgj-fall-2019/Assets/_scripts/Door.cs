using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public GameObject locked_door_sprite;
    public GameObject unlocked_door_sprite;


    public bool Locked = true;

    public int cam_x = 0;
    public int cam_y = 0;

    public float player_x = 0;
    public float player_y = 0;

    private GameObject cam;
    private GameObject player;
    public BoxCollider2D collide;

    // Start is called before the first frame update
    void Start()
    {
        collide = GetComponent<BoxCollider2D>();
        if(Locked)
        {
            LockDoor();
        }
        else
        {
            UnlockDoor();
        }
        cam = GameObject.FindGameObjectWithTag("CameraTarget");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void LockDoor()
    {
        locked_door_sprite.SetActive(true);
        unlocked_door_sprite.SetActive(false);
        collide.isTrigger = false;
    }

    public void UnlockDoor()
    {
        locked_door_sprite.SetActive(false);
        unlocked_door_sprite.SetActive(true);
        collide.isTrigger = true;
    }

    public bool IsLocked()
    {
        return Locked;
    }




    void OnTriggerEnter2D(Collider2D theCollision)
    {
        if (theCollision.tag == "Player")
        {
            cam.transform.position = new Vector3(cam.transform.position.x + cam_x, cam.transform.position.y + cam_y, cam.transform.position.z);
            player.transform.position = new Vector3(player.transform.position.x + player_x, player.transform.position.y + player_y, player.transform.position.z);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
