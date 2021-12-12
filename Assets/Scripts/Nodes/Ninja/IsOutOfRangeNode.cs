using UnityEngine;

public class IsOutOfRangeNode : Node
{
    private Transform origin;
    private Transform target;

    public IsOutOfRangeNode(Transform _origin, Transform _target)
    {
        origin = _origin;
        target = _target;
    }

    public override NodeState Evaluate()
    {
        nodeState = Vector2.Distance(origin.position, target.position) > 5 ? NodeState.success : NodeState.failure;
        return nodeState;
    }
}
