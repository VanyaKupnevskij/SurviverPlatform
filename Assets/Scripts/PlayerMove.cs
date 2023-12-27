using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speedMove = 1.5f;
    [SerializeField] private float speedRotate = 7.0f;
    [SerializeField] private PlayerInput playerInput;

    public Action<Vector3> OnGoOutside;
    public bool canControl = true;

    private Vector3 velocity;
    private bool isGrounded;
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = controller ? controller : GetComponent<CharacterController>();
        playerInput = playerInput ? playerInput : GetComponent<PlayerInput>();

        OnGoOutside += (vec) => { };
    }

    void Update()
    {
        if (canControl) 
            Movement();
    }

    private void Movement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        if (move != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, move, Time.deltaTime * speedRotate); ;
            controller.Move(transform.forward * Time.deltaTime * speedMove);
        }

        velocity.y += gravityValue * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void TeleportTo(Vector3 pos)
    {
        controller.enabled = false;
        velocity = Vector3.zero;
        transform.position = pos;
        controller.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Outside"))
        {
            OnGoOutside(transform.position);
        }
    }
}
