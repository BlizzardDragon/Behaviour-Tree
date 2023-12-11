using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using UnityEngine;

public class BehaviourNode_CheckTarget : BehaviourNode
{
    [SerializeField] private Blackboard _blackboard;


    protected override void Run()
    {
        if (!_blackboard.TryGetVariable(BlackboardKeys.TREE, out Sample.Tree tree))
        {
            Return(true);
            return;
        }

        if (tree == null)
        {
            Return(true);
        }
        else
        {
            Return(false);
        }
    }
}