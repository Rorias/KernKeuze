using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class NinjaBehaviour : AgentAI
{
    public GameObject prefabSmokeBomb;

    public Transform player;
    public Transform guard;

    public Tilemap coverTM;

    private Selector topNode;

    private Vector2 coverPos = Vector2.zero;

    private List<Vector2Int> coverSpots = new List<Vector2Int>();

    private float behaviourEvaluateCooldown = 0.0f;
    private float maxBehaviourEvaluateCooldown = 0.1f;

    private float throwBombCooldown = 0.0f;
    private float maxThrowBombCooldown = 1f;

    protected override void Awake()
    {
        base.Awake();
        moveDuration = 0.1f;
    }

    private void Start()
    {
        CreateCoverSpots();
        ConstructBehaviourTree();
    }

    protected override void Update()
    {
        base.Update();

        if (behaviourEvaluateCooldown < 0)
        {
            behaviourEvaluateCooldown = maxBehaviourEvaluateCooldown;
            topNode.Evaluate();

            if (topNode.nodeState == Node.NodeState.failure)
            {
                ShowBehaviourState("Idle");
            }
        }
    }

    private void FixedUpdate()
    {
        behaviourEvaluateCooldown -= Time.fixedDeltaTime;
        throwBombCooldown -= Time.fixedDeltaTime;
    }

    private void CreateCoverSpots()
    {
        for (int row = 0; row < coverTM.size.x; row++)
        {
            for (int col = 0; col < coverTM.size.y; col++)
            {
                if (coverTM.HasTile(new Vector3Int(row, col, 0)))
                {
                    coverSpots.Add(new Vector2Int(row, col));
                }
            }
        }
    }

    private void ConstructBehaviourTree()
    {
        IsAttackedNode isAttackedNode = new IsAttackedNode(guard, player);
        IsCoveredNode isCoveredNode = new IsCoveredNode(this, coverTM);
        GoToCoverNode goToCoverNode = new GoToCoverNode(this);
        IsOutOfRangeNode isOutOfRangeNode = new IsOutOfRangeNode(transform, player);
        FollowNode followNode = new FollowNode(player, this);
        ThrowBombNode throwBombNode = new ThrowBombNode(this);

        Sequence followPlayerSequence = new Sequence(new List<Node> { isOutOfRangeNode, followNode });

        Sequence isCoveredSequence = new Sequence(new List<Node> { isCoveredNode, throwBombNode });
        Selector tryTakeCoverSelector = new Selector(new List<Node> { isCoveredSequence, goToCoverNode });
        Sequence takeCoverSequence = new Sequence(new List<Node> { isAttackedNode, tryTakeCoverSelector });

        topNode = new Selector(new List<Node> { takeCoverSequence, followPlayerSequence });
    }

    public void GoToCover(Vector2 _coverPos)
    {
        if (coverPos != _coverPos || (coverPos == _coverPos && transform.position != (Vector3)coverPos && (path == null || path.Count == 0)))
        {
            FindPathToTarget(transform.position, _coverPos);
            coverPos = _coverPos;
        }
    }

    public void ThrowSmokeBomb()
    {
        if (throwBombCooldown < 0)
        {
            throwBombCooldown = maxThrowBombCooldown;

            GameObject smokeBomb = Instantiate(prefabSmokeBomb, transform.position, Quaternion.identity);
            smokeBomb.GetComponent<SmokeBomb>().guard = guard.GetComponent<GuardBehaviour>();
            smokeBomb.GetComponent<SmokeBomb>().targetPos = guard.transform.position;
        }
    }

    public Vector2 FindNearestTree()
    {
        float closestTreeDistance = float.PositiveInfinity;
        Vector2 treePos = Vector2.one;

        for (int i = 0; i < coverSpots.Count; i++)
        {
            float newDistance = Vector2.Distance(coverSpots[i], transform.position);

            if (newDistance < closestTreeDistance)
            {
                closestTreeDistance = newDistance;
                treePos = coverSpots[i];
            }
        }

        return new Vector2(treePos.x + 0.5f, treePos.y + 0.5f);
    }
}
