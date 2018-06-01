﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //PUBLIC VARIABLES
    [Header("Movement settings")]
    public float speed;
    public float jumpStrength;
    [Header("Raycast ground check")]
    private Transform GroundCheck;
    [Space(10)]
    public LayerMask WhatIsGround;

    //SCRIPT VARIABLES
    const float GroundedRadius = .2f;

    //REFERENCES
    Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        //SETTING UP REFERENCES
        rb = GetComponent<Rigidbody2D>();
        GroundCheck = transform.Find("GroundCheck");
    }
	
	// Update is called once per frame
	void Update () {
		
        //DETECT HORIZONTAL INPUT
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            HorizontalMovement(Input.GetAxisRaw("Horizontal"));
        }
        else
        {
            HorizontalMovement(0);
        }

        //DETECT VERTICAL INPUT
        if(Input.GetAxisRaw("Vertical") > 0)
        {
            if (IsGrounded())
            {
                Jump();
            }
        }
	}

    //JUMP
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x,jumpStrength);
    }

    //DIRECTION IS THE HORIZONTAL INPUT VALUE (-1 to 1)
    void HorizontalMovement(float direction)
    {
        rb.velocity = new Vector2(direction * speed * Time.deltaTime, rb.velocity.y);
    }

    //CHECK IF GROUNDED
    bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(GroundCheck.position, GroundedRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                return true;
        }
        return false;
    }
}
