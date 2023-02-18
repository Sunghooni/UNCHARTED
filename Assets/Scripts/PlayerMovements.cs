using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider bodyCol;

    private float moveSpeed = 5f;
    private float jumpPower = 5f;
    private Vector3 input;

    private void Update()
    {
        input = Vector3.zero;
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = transform.TransformDirection(input * moveSpeed);

        moveDir = Vector3.Lerp(rb.velocity, moveDir, Time.deltaTime * 5f);
        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
    }
}
