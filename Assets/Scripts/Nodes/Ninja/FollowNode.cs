using UnityEngine;

public class FollowNode : Node
{
    private Transform target;
    private NinjaBehaviour ninja;

    public FollowNode(Transform _target, NinjaBehaviour _ninja)
    {
        target = _target;
        ninja = _ninja;
    }
    public override NodeState Evaluate()
    {
        float distance = Vector2.Distance(ninja.transform.position, target.position);

        if (distance > 5f && distance > 2f)
        {
            ninja.ShowBehaviourState("Follow");
            ninja.FindPathToTarget(ninja.transform.position, target.position);
            nodeState = NodeState.running;
        }
        else
        {
            nodeState = NodeState.success;
        }

        return nodeState;
    }
}
