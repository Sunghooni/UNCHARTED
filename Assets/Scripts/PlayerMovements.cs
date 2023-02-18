using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody rb;
    private Collider bodyCol;
    private Animator animator;

    private float moveSpeed = 5f;
    private float jumpPower = 5f;
    private float mouseSens = 3f;

    private Vector3 moveInput;
    private Vector2 mouseInput;
    private bool jumpInput;
    private bool isGround;

    private void OnApplicationFocus(bool focus)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bodyCol = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveInput = new(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        mouseInput = new(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        
        CheckIsGround();
        CheckJump();
    }

    private void FixedUpdate()
    {
        MoveXZ();
        RotateByMouse();
    }

    private void MoveXZ()
    {
        //Movements By Rigidbody
        Vector3 moveDir = transform.TransformDirection(moveInput) * moveSpeed;

        moveDir = Vector3.Lerp(rb.velocity, moveDir, Time.deltaTime * 5f);
        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

        //Movements Blend Tree
        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);

        animator.SetFloat("MoveZ", localVel.z);
        animator.SetFloat("MoveX", localVel.x);
    }

    private void CheckJump()
    {
        bool isJumpable = jumpInput && isGround;

        //Jump Animation
        animator.SetBool("Jump", isJumpable);

        if (isJumpable)
        {
            //Add Jump Force
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void RotateByMouse()
    {
        transform.Rotate(new Vector3(0f, mouseInput.x * mouseSens, 0f));
    }

    private void CheckIsGround()
    {
        //Ground Check
        isGround = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f);
        
        //Aniamtion Bool Parameter
        animator.SetBool("IsGround", isGround);
    }
}
