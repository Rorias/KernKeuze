using UnityEngine;

public class IsInAttackRangeNode : Node
{
    private GuardBehaviour origin;
    private Transform target;
    private float range;

    public IsInAttackRangeNode(GuardBehaviour _origin, Transform _target, float _range)
    {
        origin = _origin;
        target = _target;
        range = _range;
    }

    public override NodeState Evaluate()
    {
        if (!origin.hasWeapon && nodeState == NodeState.success)
        {
            nodeState = NodeState.success;
        }
        else
        {
            nodeState = Vector2.Distance(origin.transform.position, target.position) < range ? NodeState.success : NodeState.failure;
        }

        return nodeState;
    }
}
