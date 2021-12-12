using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GuardBehaviour : AgentAI
{
    public List<Vector2> waypoints = new List<Vector2>();
    public List<GameObject> weapons = new List<GameObject>();
    public List<Vector2> smokedPositions = new List<Vector2>();

    public Transform player;

    private Selector topNode;

    [HideInInspector] public Vector2 weaponPos = Vector2.zero;
    private Vector2 lastWaypoint = Vector2.zero;

    private float behaviourEvaluateCooldown = 0.0f;
    private float maxBehaviourEvaluateCooldown = 0.1f;

    [HideInInspector] public bool hasWeapon;
    private bool chasing = false;

    protected override void Awake()
    {
        base.Awake();
        moveDuration = 0.1f;
    }

    private void Start()
    {
        ConstructBehaviourTree();
    }

    protected override void Update()
    {
        base.Update();

        if (behaviourEvaluateCooldown < 0)
        {
            behaviourEvaluateCooldown = maxBehaviourEvaluateCooldown;
            topNode.Evaluate();
        }
    }

    private void FixedUpdate()
    {
        behaviourEvaluateCooldown -= Time.fixedDeltaTime;
    }

    private void ConstructBehaviourTree()
    {
        PatrolNode patrolNode = new PatrolNode(this);
        ChaseNode chaseNode = new ChaseNode(player, this);
        IsInChaseRangeNode isInRangeNodeChase = new IsInChaseRangeNode(this, player, 5);
        IsNotInSmokeNode isNotInSmokeNode = new IsNotInSmokeNode(this);
        IsArmedNode isArmedNode = new IsArmedNode(this);
        AttackNode attackNode = new AttackNode(this);
        FindWeaponNode findWeaponNode = new FindWeaponNode(this);
        IsInAttackRangeNode isInRangeNodeAttack = new IsInAttackRangeNode(this, player, 1);

        Sequence isArmedSequence = new Sequence(new List<Node> { isArmedNode, attackNode });
        Selector tryToAttackSelector = new Selector(new List<Node> { isArmedSequence, findWeaponNode });
        Sequence attackSequence = new Sequence(new List<Node> { isInRangeNodeAttack, tryToAttackSelector });

        Sequence chaseSequence = new Sequence(new List<Node> { isNotInSmokeNode, isInRangeNodeChase, chaseNode });

        topNode = new Selector(new List<Node> { attackSequence, chaseSequence, patrolNode });
    }

    public void Patrol()
    {
        if (transform.position == (Vector3)waypoints[0])
        {
            waypoints.Add(waypoints[0]);
            waypoints.RemoveAt(0);
        }

        if (lastWaypoint != waypoints[0] || chasing)
        {
            FindPathToTarget(transform.position, waypoints[0]);
            lastWaypoint = waypoints[0];
            chasing = false;
        }
    }

    public void Chase()
    {
        chasing = true;
        FindPathToTarget(transform.position, player.position);
    }

    public Vector2 FindWeapon()
    {
        float closestWeaponDistance = float.PositiveInfinity;
        Vector2 weaponPos = Vector2.one;

        for (int i = 0; i < weapons.Count; i++)
        {
            float newDistance = Vector2.Distance(weapons[i].transform.position, transform.position);

            if (newDistance < closestWeaponDistance)
            {
                closestWeaponDistance = newDistance;
                weaponPos = weapons[i].transform.position;
            }
        }

        return weaponPos;
    }

    public void GoToWeapon(Vector2 _weaponPos)
    {
        if (weaponPos != _weaponPos)
        {
            FindPathToTarget(transform.position, _weaponPos);
            weaponPos = _weaponPos;
        }
    }

    public void GrabWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].transform.position == transform.position)
            {
                weapons[i].transform.parent = transform;
                weapons[i].transform.localPosition = new Vector2(-0.475f, 0.225f);
                hasWeapon = true;
                break;
            }
        }
    }

    public bool CheckForSmoke()
    {
        bool inSmoke = false;

        for (int i = 0; i < smokedPositions.Count; i++)
        {
            if (Vector2.Distance(smokedPositions[i], transform.position) < 1.5f)
            {
                inSmoke = true;
                break;
            }
        }

        return inSmoke;
    }
}
