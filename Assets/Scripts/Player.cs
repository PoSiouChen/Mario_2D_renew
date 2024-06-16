using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb2d;
    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f; //跳5格
    public float maxJumpTime = 1f; //跳起時間為1s
    public float JumpForce => 2f * maxJumpHeight / (maxJumpTime / 2f);
    public float gravity => -2f * maxJumpHeight / Mathf.Pow(maxJumpTime / 2f, 2);

    public bool isGrounded {get; private set;}
    public bool isJumping {get; private set;}

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
        mainCamera = Camera.main;
    }

    private void Update() 
    {
        HorizontalMove();
        isGrounded = rb2d.Raycast(Vector2.down);
        if(isGrounded)
        {
            Jump();
        }
        ApplyGravity();
    }
private void FixedUpdate()
{
    Vector2 position = rb2d.position;
    position += velocity * Time.fixedDeltaTime;
    
    // Debug the velocity and position
    Debug.Log($"Velocity: {velocity}, Position before clamp: {position}");

    Vector2 leftEdge = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
    Vector2 rightEdge = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));
    position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);

    Debug.Log($"Position after clamp: {position}");

    rb2d.MovePosition(position);
}

    private void HorizontalMove()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.Lerp(velocity.x, inputAxis * moveSpeed, Time.deltaTime * 15);
    }

    private void Jump()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        isJumping = velocity.y > 0f;

        if(Input.GetButtonDown("Jump"))
        {
            Debug.Log("down Jump");
            velocity.y = JumpForce;
            isJumping = true;
        }
    }
    private void ApplyGravity()
    {
        bool falling = velocity.y < 0 || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

}
