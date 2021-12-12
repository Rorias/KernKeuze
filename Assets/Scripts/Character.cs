using UnityEngine;

public class Character : MonoBehaviour
{
    protected Movement movement;

    [HideInInspector] public bool isMoving = false;

    protected Vector2 lastDirection;
    protected float moveDuration;
    protected Vector2 CharDim;
    protected int xOffset;

    protected virtual void Awake()
    {
        movement = Movement.Instance;
    }
}
