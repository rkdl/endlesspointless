using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;


public class OldManScript : MonoBehaviour
{
    public float moveCooldown = 5;
    public float moveSpeed = 2;

    private float moveCooldownLeft = 0;
    private Animator animator;
    private Vector3 destination;
    private Vector3 startPosition;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        var housePos = GameObject.Find("house-c").transform.position;
        destination = new Vector3(housePos.x, transform.position.y);
        startPosition = transform.position;
    }
    
    void Update()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        var isDestinationReached = transform.position == destination;

        animator.SetBool("isMoving", !isDestinationReached && moveCooldownLeft <= 0);

        if (isDestinationReached)
        {
            moveCooldownLeft = moveCooldown;
            var prevDest = destination;
            destination = startPosition;
            startPosition = prevDest;
            return;
        }

        if (moveCooldownLeft > 0)
        {
            moveCooldownLeft -= Time.deltaTime;
            return;
        }

        var isFacingRight = Mathf.Approximately(transform.localEulerAngles.y, 0);
        var isFacingLeft = !isFacingRight;
        var shouldMoveLeft = transform.localEulerAngles.x > destination.x;
        var shouldMoveRight = transform.localEulerAngles.x < destination.x;
        if (shouldMoveLeft && isFacingRight || shouldMoveRight && isFacingLeft)
        {
            CharacterUtils.TurnAround(transform);
        }

        transform.position = Vector3.MoveTowards(
            transform.position, destination, Time.deltaTime * moveSpeed);
    }
}
