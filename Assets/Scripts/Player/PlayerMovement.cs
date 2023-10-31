using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Character Movement")]
    public float speed = 5f;
    public float speedMultiplicator = 1f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] CharacterController controller;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [Header("Inputs")]
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference jump;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = movement.action.ReadValue<Vector2>().x;
        float z = movement.action.ReadValue<Vector2>().y;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(speed * speedMultiplicator * Time.deltaTime * move);

        if (jump.action.IsPressed() && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
