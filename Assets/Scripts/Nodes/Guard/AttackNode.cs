public class AttackNode : Node
{
    private GuardBehaviour guard;

    public AttackNode(GuardBehaviour _guard)
    {
        guard = _guard;
    }

    public override NodeState Evaluate()
    {
        guard.ShowBehaviourState("Attack");

        nodeState = NodeState.running;
        return nodeState;
    }
}
