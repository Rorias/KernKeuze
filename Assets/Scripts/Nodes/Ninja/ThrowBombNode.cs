public class ThrowBombNode : Node
{
    private NinjaBehaviour ninja;

    public ThrowBombNode(NinjaBehaviour _ninja)
    {
        ninja = _ninja;
    }

    public override NodeState Evaluate()
    {
        ninja.ShowBehaviourState("Throw Bomb");
        ninja.ThrowSmokeBomb();

        nodeState = NodeState.running;
        return nodeState;
    }
}
