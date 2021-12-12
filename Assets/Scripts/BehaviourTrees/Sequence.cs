using System.Collections.Generic;

public class Sequence : Node
{
    protected List<Node> childNodes = new List<Node>();

    public Sequence(List<Node> _childNodes)
    {
        childNodes = _childNodes;
    }

    public override NodeState Evaluate()
    {
        bool isAnyChildRunning = false;

        for (int i = 0; i < childNodes.Count; i++)
        {
            switch (childNodes[i].Evaluate())
            {
                case NodeState.running:
                    isAnyChildRunning = true;
                    break;
                case NodeState.success:
                    break;
                case NodeState.failure:
                    nodeState = NodeState.failure;
                    return nodeState;
                default:
                    break;
            }
        }

        nodeState = isAnyChildRunning ? NodeState.running : NodeState.success;
        return nodeState;
    }
}
