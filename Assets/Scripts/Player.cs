using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb2d;
    private float inputAxis;

    public float moveSpeed = 8f;
    public float jumpForce = 15f;
    public float gravityScale = 3f; 

    public float groundCheckDistance = 1f; 
    public LayerMask groundMask; 


    public bool isGrounded { get; private set; }
    public bool isJumping { get; private set; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = gravityScale;
        mainCamera = Camera.main;
    }

    private void Update() 
    {
        HorizontalMove();
        isGrounded = CheckGrounded();
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HorizontalMove()
    {
        inputAxis = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(Mathf.Lerp(rb2d.velocity.x, inputAxis * moveSpeed, Time.deltaTime * 15), rb2d.velocity.y);
    }

    private void Jump()
    {
        isJumping = true;
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    private bool CheckGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundCheckDistance, groundMask);
        Debug.DrawRay(position, direction * groundCheckDistance, Color.red, 0.1f);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    private void Move()
    {
        Vector2 position = rb2d.position;
        
        Vector2 leftEdge = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 rightEdge = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);

        rb2d.position = position;
    }
}
