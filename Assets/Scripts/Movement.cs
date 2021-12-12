using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement
{
    #region Singleton
    private static Movement _instance;
    private static object _lock = new object();

    private Movement() { }

    public static Movement Instance
    {
        get
        {
            if (null == _instance)
            {
                lock (_lock)
                {
                    if (null == _instance)
                    {
                        _instance = new Movement();
                    }
                }
            }
            return _instance;
        }
    }
    #endregion

    public Tilemap collisionTM;

    public bool CollidingIn(Transform _char, Vector3 _direction, Vector2 _charDim, int _xCenter)
    {
        Vector3Int position = collisionTM.WorldToCell(_char.position + _direction);

        bool occupied = false;

        for (int row = -_xCenter; row < _charDim.x - _xCenter; row++)
        {
            for (int col = 0; col < _charDim.y; col++)
            {
                if (occupied) { break; }

                occupied |= collisionTM.HasTile(position + new Vector3Int(row, col, 0));
            }
        }

        return occupied;
    }

    public IEnumerator MoveCharacter(Character _char, Vector2 _direction, float _timeToMove)
    {
        _char.isMoving = true;
        float elapsedTime = 0f;
        Vector2 originalPosition = _char.transform.position;
        Vector2 targetPosition = originalPosition + _direction;
        while (elapsedTime < _timeToMove)
        {
            _char.transform.position = Vector2.Lerp(originalPosition, targetPosition, elapsedTime / _timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _char.transform.position = targetPosition;
        _char.isMoving = false;
        yield break;
    }
}
