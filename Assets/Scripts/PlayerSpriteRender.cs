using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRender : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement movement;
    private Animator animator;

    public Sprite idle;
    public Sprite jump;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator.enabled = false;
    }

    private void LateUpdate()
    {
        if (!movement.isGrounded) {
            animator.enabled = false;
            spriteRenderer.sprite = jump;
        } else if (movement.isRunning) {
            animator.enabled = true;
        } else {
            animator.enabled = false;
            spriteRenderer.sprite = idle;
        }
    }
}
