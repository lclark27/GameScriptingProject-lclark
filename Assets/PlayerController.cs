using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //character controller setup
    CharacterController controller;

  
    public float speed;

    public float jumpSpeed;
    public float gravity;

    //value for movement direction,  .zero avoids the null value
    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //obtain character controller
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if the player is on the ground
        if (controller.isGrounded)
        {
            //if player is on the ground, input rotation and movement
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //move character in the chosen direction * by speed
            moveDirection *= speed;

            //player jumping function
            if(Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        //mechanic to bring the player back to the ground
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
