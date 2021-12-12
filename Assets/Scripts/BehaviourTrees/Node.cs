using System;

[Serializable]
public abstract class Node
{
    public enum NodeState
    {
        running, success, failure,
    }

    public NodeState nodeState { get; protected set; }

    public abstract NodeState Evaluate();
}
