using UnityEngine;

public class IsInChaseRangeNode : Node
{
    private GuardBehaviour origin;
    private Transform target;
    private float range;

    public IsInChaseRangeNode(GuardBehaviour _origin, Transform _target, float _range)
    {
        origin = _origin;
        target = _target;
        range = _range;
    }

    public override NodeState Evaluate()
    {
        nodeState = Vector2.Distance(origin.transform.position, target.position) < range ? NodeState.success : NodeState.failure;

        return nodeState;
    }
}
