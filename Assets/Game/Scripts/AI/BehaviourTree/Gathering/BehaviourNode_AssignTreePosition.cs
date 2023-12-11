using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using UnityEngine;
using Tree = Sample.Tree;

public class BehaviourNode_AssignTreePosition : BehaviourNode
{
    [SerializeField] private Blackboard _blackboard;


    protected override void Run()
    {
        if (!_blackboard.TryGetVariable(BlackboardKeys.TREE, out Tree tree) ||
        !_blackboard.TryGetVariable(BlackboardKeys.TREE_STOPING_DISTANCE, out float treeStopingDistance))
        {
            Return(false);
            return;
        }

        var treePosition = tree.transform.position;
        _blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, treePosition);
        _blackboard.SetVariable(BlackboardKeys.STOPING_DISTANCE, treeStopingDistance);

        Return(true);
    }
}