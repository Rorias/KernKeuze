using System.Collections.Generic;

public class Selector : Node
{
    protected List<Node> childNodes = new List<Node>();

    public Selector(List<Node> _childNodes)
    {
        childNodes = _childNodes;
    }

    public override NodeState Evaluate()
    {
        for (int i = 0; i < childNodes.Count; i++)
        {
            switch (childNodes[i].Evaluate())
            {
                case NodeState.running:
                    nodeState = NodeState.running;
                    return nodeState;
                case NodeState.success:
                    nodeState = NodeState.success;
                    return nodeState;
                case NodeState.failure:
                    break;
                default:
                    break;
            }
        }

        nodeState = NodeState.failure;
        return nodeState;
    }
}
