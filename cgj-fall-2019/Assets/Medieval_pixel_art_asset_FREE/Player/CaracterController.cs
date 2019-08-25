﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private float _horizontal;
    private float _vertical;

    public float walkSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        _rigidbody.velocity = new Vector2(_horizontal, _vertical).normalized * walkSpeed;
        var velocity = _rigidbody.velocity;
        if (velocity.magnitude > 0)
        {
            var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            _sprite.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
