using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class Player_Script : NetworkBehaviour
{
    [SerializeField]
    float MoveSpeed = 2f; 

    CinemachineFreeLook vcam;

    [SerializeField]
    CharacterController controller;

    Camera cam;

    float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    [SerializeField]
    float jumpSpeed = 2.0f;

    [SerializeField]
    float gravity = 10.0f;

    private Vector3 movingDirection = Vector3.zero;

    Animator animator;

    bool isRunning;


    void Awake()
    {
        cam = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();    
    }

 
    public override void OnStartLocalPlayer()     //as the player in a prefab assing its transform to the CineMachineFreelook Camera when it is instantiated in the scene. 
    {

        vcam = FindObjectOfType<CinemachineFreeLook>();
        vcam.Follow = transform;
        vcam.LookAt = transform;

    }

  
    void Update()
    {
        if (!isLocalPlayer) { return; }  


        float moveHor = Input.GetAxisRaw("Horizontal");            
        float moveVer = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(moveHor, 0f, moveVer).normalized;   //Normalising to constrain the diagonal movement.

        //Checking for any input from players.

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y; //Finding the angle between forward Z and forward of the player.Then converting the radian value to degrees.
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);// Damping and smothing the value for rotation.
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * MoveSpeed * Time.deltaTime);//using Character Controller to move player desirably.

            animator.SetBool("isRunning", true);//Setting the animation state of 'isRunning' to True. 
        }
        else
        {
            animator.SetBool("isRunning", false);//Setting the animation state of 'isRunning' to False.
        }

        isRunning = animator.GetBool("isRunning");


        if (controller.isGrounded && Input.GetButtonDown("Jump"))   //Checking for ground and taking input for the jump action.
        {
            movingDirection.y = jumpSpeed;
            animator.SetBool("isJumping", true);
           
        }
        else if (controller.isGrounded && !Input.GetButtonDown("Jump")) 
        {
        
            animator.SetBool("isJumping", false);

        }
        
        movingDirection.y -= gravity * Time.deltaTime;
        controller.Move(movingDirection * Time.deltaTime);
    }
}
