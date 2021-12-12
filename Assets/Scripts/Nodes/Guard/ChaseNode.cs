using UnityEngine;

public class ChaseNode : Node
{
    private Transform target;
    private GuardBehaviour guard;

    public ChaseNode(Transform _target, GuardBehaviour _guard)
    {
        target = _target;
        guard = _guard;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector2.Distance(guard.transform.position, target.position);

        guard.ShowBehaviourState("Chase");
        guard.Chase();

        nodeState = NodeState.running;
        return nodeState;
    }
}
