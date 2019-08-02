using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;

    public float speed;
    public float jumpSpeed;
    public float gravity;

    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //trigger to check if player is on the ground
        if(controller.isGrounded)
        {
            //if yes, check inputs for rotation/movement
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //calculate direction * speed
            moveDirection *= speed;

            //Jumping function
            if(Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }


        //Apply gravity function
        moveDirection.y -= gravity * Time.deltaTime;
        //Move our character
        controller.Move(moveDirection * Time.deltaTime);

    }
}
