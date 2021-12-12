public class IsArmedNode : Node
{
    private GuardBehaviour guard;

    public IsArmedNode(GuardBehaviour _guard)
    {
        guard = _guard;
    }

    public override NodeState Evaluate()
    {
        nodeState = guard.hasWeapon ? NodeState.success : NodeState.failure;
        return nodeState;
    }
}
