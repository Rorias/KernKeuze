using UnityEngine;

public class IsAttackedNode : Node
{
    private Transform attacker;
    private Transform player;

    public IsAttackedNode(Transform _attacker, Transform _player)
    {
        attacker = _attacker;
        player = _player;
    }

    public override NodeState Evaluate()
    {
        nodeState = Vector2.Distance(attacker.position, player.position) < 1.5f ? NodeState.success : NodeState.failure;
        return nodeState;
    }
}
