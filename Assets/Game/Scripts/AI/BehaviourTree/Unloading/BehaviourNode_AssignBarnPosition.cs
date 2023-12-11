using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;

public class BehaviourNode_AssignBarnPosition : BehaviourNode
{
    [SerializeField] private Blackboard _blackboard;


    protected override void Run()
    {
        if (!_blackboard.TryGetVariable(BlackboardKeys.BARN, out Barn barn) ||
        !_blackboard.TryGetVariable(BlackboardKeys.BARN_STOPING_DISTANCE, out float barnStopingDistance))
        {
            Return(false);
            return;
        }

        var barnPosition = barn.transform.position;
        _blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, barnPosition);
        _blackboard.SetVariable(BlackboardKeys.STOPING_DISTANCE, barnStopingDistance);

        Return(true);
    }
}