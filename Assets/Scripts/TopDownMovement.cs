using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownController controller;
    private Rigidbody2D movementRigidbody;

    private Vector2 movementDirection = Vector2.zero;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private float gridSize = 1.0f; // 그리드 셀의 크기를 정의

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        controller.OnMoveEvnet += Move;
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void Move(Vector2 direction)
    {
        if (!isMoving)
        {
            movementDirection = direction;
            if (movementDirection != Vector2.zero)
            {
                targetPosition = (Vector2)transform.position + movementDirection * gridSize;
                targetPosition.x = Mathf.Round(targetPosition.x / gridSize) * gridSize;
                targetPosition.y = Mathf.Round(targetPosition.y / gridSize) * gridSize;
                isMoving = true;
            }
        }
    }

    private void MoveTowardsTarget()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, 5 * Time.fixedDeltaTime);
        movementRigidbody.MovePosition(newPosition);

        if ((Vector2)transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
}


/*
public class TopDownMovement : MonoBehaviour
{
    private TopDownController controller;
    private Rigidbody2D movementRigidbody;

    private Vector2 movementDirection = Vector2.zero;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        controller.OnMoveEvnet += Move;
    }



    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction  ;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * 5;
        movementRigidbody.velocity = direction;
    }
}
*/