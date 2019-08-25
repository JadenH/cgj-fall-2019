﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public Door top_door;
    public Door right_door;
    public Door bottom_door;
    public Door left_door;

    // Start is called before the first frame update
    void Start()
    {
        // top_door = transform.GetChild(0).gameObject.GetComponent<Door>();
     //    right_door = transform.GetChild(1).gameObject.GetComponent<Door>();
      //   bottom_door = transform.GetChild(2).gameObject.GetComponent<Door>();
       //  left_door = transform.GetChild(3).gameObject.GetComponent<Door>();
    }
    //Top
    public bool Is_locked_top_door()
    { 
        return top_door.IsLocked();
    }
    public void Lock_top_door()
    {
        top_door.LockDoor();
    }
    public void Unlock_top_door()
    {
        top_door.UnlockDoor();
    }
    //Right
    public bool Is_locked_right_door()
    {
        return 
            right_door.IsLocked();
    }
    public void Lock_right_door()
    {
        right_door.LockDoor();
    }
    public void Unlock_right_door()
    {
        right_door.UnlockDoor();
    }
    //Bottom
    public bool Is_locked_bottom_door()
    {
        return bottom_door.IsLocked();
    }
    public void Lock_bottom_door()
    {
        bottom_door.LockDoor();
    }
    public void Unlock_bottom_door()
    {
        bottom_door.UnlockDoor();
    }
    //Left
    public bool Is_locked_left_door()
    {
        return left_door.IsLocked();
    }
    public void Lock_left_door()
    {
        left_door.LockDoor();
    }
    public void Unlock_left_door()
    {
        left_door.UnlockDoor();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
