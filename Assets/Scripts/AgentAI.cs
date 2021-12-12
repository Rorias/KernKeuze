using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class AgentAI : Character
{
    public Text behaviourText;

    [HideInInspector] public Map map;
    protected List<Vector2> path = new List<Vector2>();

    protected override void Awake()
    {
        base.Awake();
        map = GameObject.Find("GameManager").GetComponent<Map>();
    }

    protected virtual void Update()
    {
        if (path != null && path.Count > 0)
        {
            if (transform.position != (Vector3)path[0] && !isMoving)
            {
                StartCoroutine(movement.MoveCharacter(this, new Vector2(
                    path[0].x - transform.position.x,
                    path[0].y - transform.position.y), 0.1f));
            }
            else if (!isMoving)
            {
                path.RemoveAt(0);
            }
        }

        UpdateBehaviourTextPos();
    }

    public void FindPathToTarget(Vector2 startPos, Vector2 endPos)
    {
        path = AStar.Instance.FindPathToTarget(new Vector2Int(Mathf.RoundToInt(startPos.x - 0.5f), Mathf.RoundToInt(startPos.y - 0.5f)), new Vector2Int(Mathf.RoundToInt(endPos.x - 0.5f), Mathf.RoundToInt(endPos.y - 0.5f)), map.map);
    }

    public void UpdateBehaviourTextPos()
    {
        behaviourText.rectTransform.anchoredPosition = new Vector2(transform.position.x - 26, transform.position.y - 22.5f);
    }

    public void ShowBehaviourState(string _currentBehaviour)
    {
        behaviourText.text = _currentBehaviour;
    }
}
