public class PatrolNode : Node
{
    private GuardBehaviour guard;

    public PatrolNode (GuardBehaviour _guard)
    {
        guard = _guard;
    }

    public override NodeState Evaluate()
    {
        guard.ShowBehaviourState("Patrol");
        guard.Patrol();

        nodeState = NodeState.running;
        return nodeState;
    }
}
