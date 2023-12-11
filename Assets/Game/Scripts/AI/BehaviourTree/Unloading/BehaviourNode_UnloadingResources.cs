using System.Collections;
using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;

public class BehaviourNode_UnloadingResources : BehaviourNode
{
    [SerializeField] private Blackboard _blackboard;
    private int _resources;


    protected override void Run()
    {
        if (!_blackboard.TryGetVariable(BlackboardKeys.BARN, out Barn barn) ||
        !_blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character character))
        {
            Return(false);
            return;
        }

        _resources = character.UnloadResources();

        if (barn.CanAddResources(_resources))
        {
            barn.AddResources(_resources);
            Return(true);
        }
        else
        {
            StartCoroutine(WaitingUnloadResources(barn));
        }
    }

    private IEnumerator WaitingUnloadResources(Barn barn)
    {
        while (true)
        {
            if (!barn.CanAddResources(_resources))
            {
                yield return null;
            }
            else
            {
                barn.AddResources(_resources);
                Return(true);
                break;
            }
        }
    }
}