using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FixedMovement : MonoBehaviour
{
    
    public CinemachineSmoothPath dollyTrack;
    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    public float gravity = -9.81f;
    private Vector3 trackDirection;

    private CharacterController controller;
    private Vector3 playerInput;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
       
        // Get the position of the camera on the dolly track
        float trackPosition = Camera.main.GetComponent<CinemachineDollyCart>().m_Position;

        // Get the direction of the dolly track at the camera's position
        trackDirection = dollyTrack.EvaluateOrientation(trackPosition).eulerAngles;

        // Get the player input vector
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        playerInput = new Vector3(horizontalInput, 0f, verticalInput);

        // Project the player input onto the horizontal plane
        Vector3 horizontalPlayerInput = Vector3.ProjectOnPlane(playerInput, Vector3.up);

        // Calculate the dot product between the track direction and the horizontal player input
        float dotProduct = Vector3.Dot(trackDirection, horizontalPlayerInput);

        // If the dot product is negative, flip the direction of the player input
        if (dotProduct < 0f)
        {
            playerInput *= -1f;
        }

        // Apply gravity to the player
        Vector3 velocity = playerInput * moveSpeed;
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Jump if the player presses the jump button
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = jumpSpeed;
        }

        // Move the player using the character controller
        controller.Move(velocity * Time.deltaTime);
    }
}



//void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = true;
//        }
//     //   if (!SpawnPoint)
//       // {
//         //   isSpawn = true;
//        //}
//        if (collision.gameObject.CompareTag("Finish"))
//        {
//           // CARICA SCENA QUI Application.LoadLevel("Level_2");
//        }
//    }

//    void OnCollisionExit(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = false;
//        }
//        if (!SpawnPoint)
//        {
//            //isSpawn = true;
//        }
//    }
//}