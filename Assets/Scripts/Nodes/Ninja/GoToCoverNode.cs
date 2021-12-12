using UnityEngine;

public class GoToCoverNode : Node
{
    private NinjaBehaviour ninja;

    public GoToCoverNode(NinjaBehaviour _ninja)
    {
        ninja = _ninja;
    }

    public override NodeState Evaluate()
    {
        Vector3 coverPos = ninja.FindNearestTree();

        if (ninja.transform.position != coverPos)
        {
            ninja.ShowBehaviourState("Seek Cover");
            ninja.GoToCover(coverPos);
            nodeState = NodeState.running;
            return nodeState;
        }
        else
        {
            nodeState = NodeState.success;
            return nodeState;
        }
    }
}
