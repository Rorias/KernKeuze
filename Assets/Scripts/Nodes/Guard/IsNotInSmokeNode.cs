using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsNotInSmokeNode : Node
{
    private GuardBehaviour guard;

    public IsNotInSmokeNode(GuardBehaviour _guard)
    {
        guard = _guard;
    }

    public override NodeState Evaluate()
    {
        nodeState = guard.CheckForSmoke() ? NodeState.failure : NodeState.success;

        return nodeState;
    }
}
