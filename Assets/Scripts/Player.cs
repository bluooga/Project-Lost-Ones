using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Components
    CharacterController controller;

    //Private Variables
    Vector3 moveDirection;
    private float currentSpeed;
    private bool isRunning;

    //serializable Variables
    [SerializeField] [Range(1, 10)] public float WalkSpeed;
    [SerializeField] [Range(1, 30)] public float RunSpeed;
    [SerializeField] [Range(1, 20)] public float gravity;
    [SerializeField] [Range(1, 20)] public float jumpForce;
    [SerializeField] [Range(1, 20)] public float wallJumpForce;
    [SerializeField] [Range(1, 20)] public float wallJumpSpeed;
    [SerializeField] [Range(1, 20)] public float wallJumpSprintSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        isRunning = Input.GetButton("Fire3");

        MoveCharacter();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }

    void MoveCharacter()
    {

        if (controller.isGrounded)
        {
            currentSpeed = ((isRunning) ? RunSpeed : WalkSpeed);

            moveDirection.z = Input.GetAxisRaw("Horizontal") * currentSpeed;
        }

        
        moveDirection.y += -gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);

        if (controller.isGrounded)
        {
            moveDirection.y = 0;
        }
    }

    void Jump()
    {
        if(controller.isGrounded){
            moveDirection.y = jumpForce;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.normal.y < 0.1f)
        {
            if(hit.normal.z > 0.1f || hit.normal.z < -0.1f) {
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.DrawRay(hit.point, hit.normal, Color.red, 2.5f);
                    Debug.Log(hit.normal);

                    moveDirection.y = wallJumpForce;

                    if (!isRunning)
                    {
                        currentSpeed = wallJumpSpeed;
                    }
                    else if (isRunning)
                    {
                        currentSpeed = wallJumpSprintSpeed;
                    }

                    moveDirection.z = hit.normal.z * currentSpeed;
                }
            }
        }
    }
}

