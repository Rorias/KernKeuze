using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;

    public Vector2 xBounds;
    public Vector2 yBounds;

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Min(Mathf.Max(target.position.x, xBounds.x), xBounds.y),
                                         Mathf.Min(Mathf.Max(target.position.y, yBounds.x), yBounds.y), -10);
    }
}
