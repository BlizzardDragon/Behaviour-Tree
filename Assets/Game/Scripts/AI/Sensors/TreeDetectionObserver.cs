using Elementary;
using Lessons.AI.HierarchicalStateMachine;
using UnityEngine;
using Tree = Sample.Tree;

public class TreeDetectionObserver : ColliderDetectionObserver
{
    [SerializeField] private Blackboard _blackboard;


    protected override void OnCollidersUpdated(Collider[] buffer, int size)
    {
        if (!_blackboard.HasVariable(BlackboardKeys.TREE))
        {
            if (FindTarget(buffer, size, out Tree tree))
            {
                _blackboard.SetVariable(BlackboardKeys.TREE, tree);
            }
        }
        else
        {
            Tree tree = _blackboard.GetVariable<Tree>(BlackboardKeys.TREE);
            if (!IsTargetExists(buffer, size, tree))
            {
                _blackboard.RemoveVariable(BlackboardKeys.TREE);
            }
        }
    }

    private bool FindTarget(Collider[] buffer, int size, out Tree target)
    {
        target = null;
        float nearestDistance = float.MaxValue;
        Vector3 currentPosition = _sensor.transform.position;

        for (var i = 0; i < size; i++)
        {
            var collder = buffer[i];
            if (collder.TryGetComponent(out Tree tree))
            {
                float distance = Vector3.Distance(currentPosition, tree.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    target = tree;
                }
            }
        }

        if (target != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsTargetExists(Collider[] buffer, int size, Tree target)
    {
        for (var i = 0; i < size; i++)
        {
            var collder = buffer[i];
            if (collder.TryGetComponent(out Tree tree) && target == tree)
            {
                return true;
            }
        }

        return false;
    }
}
