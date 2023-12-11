using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;

public class BehaviourNode_CheckBag : BehaviourNode
{
    [SerializeField] private bool _bagIsFull;
    [SerializeField] private Blackboard _blackboard;


    protected override void Run()
    {
        if (!_blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character character))
        {
            Return(false);
            return;
        }

        if (character.IsResourceBagFull() == _bagIsFull)
        {
            Return(true);
        }
        else
        {
            Return(false);
        }
    }
}