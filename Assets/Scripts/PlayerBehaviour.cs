using UnityEngine;

public class PlayerBehaviour : Character
{
    protected override void Awake()
    {
        base.Awake();
        moveDuration = 0.1f;
        CharDim = new Vector2(1, 1);
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !isMoving &&
            !movement.CollidingIn(transform, Vector2.up, CharDim, xOffset))
        {
            lastDirection = Vector2.up;
            StartCoroutine(movement.MoveCharacter(this, lastDirection, moveDuration));
        }

        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !isMoving &&
            !movement.CollidingIn(transform, Vector2.down, CharDim, xOffset))
        {
            lastDirection = Vector2.down;
            StartCoroutine(movement.MoveCharacter(this, lastDirection, moveDuration));
        }

        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !isMoving &&
            !movement.CollidingIn(transform, Vector2.right, CharDim, xOffset))
        {
            lastDirection = Vector2.right;
            StartCoroutine(movement.MoveCharacter(this, lastDirection, moveDuration));
        }

        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !isMoving &&
            !movement.CollidingIn(transform, Vector2.left, CharDim, xOffset))
        {
            lastDirection = Vector2.left;
            StartCoroutine(movement.MoveCharacter(this, lastDirection, moveDuration));
        }
    }
}
