public class Inverter : Node
{
    protected Node childNode;

    public Inverter(Node _childNode)
    {
        childNode = _childNode;
    }

    public override NodeState Evaluate()
    {
        switch (childNode.Evaluate())
        {
            case NodeState.running:
                nodeState = NodeState.running;
                return nodeState;
            case NodeState.success:
                nodeState = NodeState.failure;
                return nodeState;
            case NodeState.failure:
                nodeState = NodeState.success;
                return nodeState;
            default:
                break;
        }

        return nodeState;
    }
}
