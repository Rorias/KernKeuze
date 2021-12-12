using UnityEngine.Tilemaps;

public class IsCoveredNode : Node
{
    private NinjaBehaviour ninja;
    private Tilemap coverTM;

    public IsCoveredNode(NinjaBehaviour _ninja, Tilemap _coverTM)
    {
        ninja = _ninja;
        coverTM = _coverTM;
    }

    public override NodeState Evaluate()
    {
        if (coverTM.HasTile(coverTM.WorldToCell(ninja.transform.position)))
        {
            nodeState = NodeState.success;
            return nodeState;
        }

        nodeState = NodeState.failure;
        return nodeState;
    }
}
