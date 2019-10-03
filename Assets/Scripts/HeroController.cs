using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Animations;

public class HeroController : MonoBehaviour
{
    public float moveSpeed = 1;
    public float crouchSpeed = 0.5f;
    public LayerMask whatIsGround;

    private Animator animator;
    private Rigidbody2D rigidbody;
    private float horizontalMove = 0;
    private bool isCrouching = false;
    private bool isJumping = false;
    private bool isGrounded = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void ReadInputs()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        isCrouching = Input.GetAxisRaw("Vertical") < 0;
        isJumping = Input.GetButtonDown("Jump");

        animator.SetBool("isMoving", Mathf.Abs(horizontalMove) > 0);
        animator.SetBool("isCrouching", isCrouching);
        animator.SetBool("isJumping", isJumping);
    }

    private void Update()
    {
        ReadInputs();

        var isFacingRight = Mathf.Approximately(transform.localEulerAngles.y, 0);
        var isFacingLeft = !isFacingRight;
        var shouldMoveLeft = horizontalMove < 0;
        var shouldMoveRight = horizontalMove > 0;
        if (shouldMoveLeft && isFacingRight || shouldMoveRight && isFacingLeft)
        {
            CharacterUtils.TurnAround(transform);
        }
    }

    private void FixedUpdate()
    {
        var baseSpeed = isCrouching ? crouchSpeed : moveSpeed;
        isGrounded = rigidbody.IsTouchingLayers(whatIsGround);
        rigidbody.velocity = new Vector2(baseSpeed * horizontalMove, rigidbody.velocity.y);
    }
}
