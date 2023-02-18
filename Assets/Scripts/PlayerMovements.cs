using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider bodyCol;

    private float moveSpeed = 5f;
    private float jumpPower = 5f;
    private float mouseSens = 3f;

    private Vector3 moveInput;
    private Vector2 mouseInput;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        moveInput = new(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        mouseInput = new(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = transform.TransformDirection(moveInput) * moveSpeed;

        moveDir = Vector3.Lerp(rb.velocity, moveDir, Time.deltaTime * 5f);
        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

        transform.Rotate(new Vector3(0f, mouseInput.x * mouseSens, 0f));
    }
}
