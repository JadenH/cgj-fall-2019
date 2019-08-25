﻿using UnityEngine;

public class Door : GameBehaviour
{
    private readonly bool _locked = true;

    public int CamX = 0;
    public int CamY = 0;
    public BoxCollider2D Collide;

    public GameObject LockedDoorSprite;

    public float PlayerX = 0;
    public float PlayerY = 0;
    public GameObject UnlockedDoorSprite;

    // Start is called before the first frame update
    private void Start()
    {
    }

    public void LockDoor()
    {
        LockedDoorSprite.SetActive(true);
        UnlockedDoorSprite.SetActive(false);
        Collide.isTrigger = false;
    }

    public void UnlockDoor()
    {
        LockedDoorSprite.SetActive(false);
        UnlockedDoorSprite.SetActive(true);
        Collide.isTrigger = true;
    }

    public bool IsLocked()
    {
        return _locked;
    }

    private void OnTriggerEnter2D(Collider2D theCollision)
    {
        if (theCollision.tag == "Player")
        {
            CameraTarget.transform.position = new Vector3(CameraTarget.transform.position.x + CamX,
                CameraTarget.transform.position.y + CamY,
                CameraTarget.transform.position.z);
            Player.transform.position = new Vector3(Player.transform.position.x + PlayerX,
                Player.transform.position.y + PlayerY, Player.transform.position.z);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}