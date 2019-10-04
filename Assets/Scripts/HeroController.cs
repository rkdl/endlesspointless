using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Experimental.XR;

public class HeroController : MonoBehaviour
{
    public float moveSpeed = 3;
    public float crouchSpeed = 1.25f;
    public float jumpForce = 600;
    public LayerMask whatIsGround;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    private CircleCollider2D groundCollider;
    private float horizontalMove = 0;
    private bool isCrouching = false;
    private bool jumpPressed = false;
    private bool isGrounded = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        groundCollider = GetComponent<CircleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void ReadInputs()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        isCrouching = Input.GetAxisRaw("Vertical") < 0;
        jumpPressed = Input.GetButtonDown("Jump");

        animator.SetBool("isMoving", Mathf.Abs(horizontalMove) > 0);
        animator.SetBool("isCrouching", isCrouching);
        animator.SetBool("isJumping", !isGrounded);
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

    private bool IsGrounded()
    {
        var overlappingColliders = Physics2D.OverlapCircleAll(
            groundCollider.bounds.center, groundCollider.radius + 0.3f, whatIsGround);
        return overlappingColliders.Any(col => col != groundCollider);
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();

        if (isGrounded && jumpPressed)
        {
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }

        var baseSpeed = isCrouching ? crouchSpeed : moveSpeed;
        var horizontalSpeed = baseSpeed * horizontalMove;
        rigidbody2D.velocity = new Vector2(horizontalSpeed, rigidbody2D.velocity.y);
    }
}
