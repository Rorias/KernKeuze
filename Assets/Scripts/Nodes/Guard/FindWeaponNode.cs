using UnityEngine;

public class FindWeaponNode : Node
{
    private GuardBehaviour guard;

    public FindWeaponNode(GuardBehaviour _guard)
    {
        guard = _guard;
    }

    public override NodeState Evaluate()
    {
        Vector3 weaponPos = guard.FindWeapon();

        if (guard.transform.position != weaponPos)
        {
            guard.ShowBehaviourState("Find Weapon");
            guard.GoToWeapon(weaponPos);
            nodeState = NodeState.running;
            return nodeState;
        }
        else
        {
            guard.GrabWeapon();
            nodeState = NodeState.success;
            return nodeState;
        }
    }
}
