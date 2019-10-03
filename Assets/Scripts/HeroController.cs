using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class HeroController : MonoBehaviour
{
    private const float MOVE_SPEED = 10;

    private Animator animator;
    private float horizontalMove = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * MOVE_SPEED;

        animator.SetBool("isMoving", Mathf.Abs(horizontalMove) > 0);

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
        var previousPosition = transform.position;
        transform.position = new Vector2(previousPosition.x + horizontalMove, previousPosition.y);
    }
}
