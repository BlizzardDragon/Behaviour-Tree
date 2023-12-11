using System.Collections;
using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using UnityEngine;
using Tree = Sample.Tree;

public class BehaviourNode_WaitForGatheringPeriod : BehaviourNode
{
    [SerializeField] private Blackboard _blackboard;
    private Coroutine _coroutine;


    protected override void Run()
    {
        if (!_blackboard.TryGetVariable(BlackboardKeys.GATHERING_PERIOD, out float period) ||
        !_blackboard.TryGetVariable(BlackboardKeys.TREE, out Tree tree))
        {
            Return(false);
            return;
        }

        _coroutine = StartCoroutine(WaitingProcess(tree, period));
    }

    protected override void OnAbort()
    {
        StopCoroutine(_coroutine);
        Return(false);
    }

    private IEnumerator WaitingProcess(Tree tree, float period)
    {
        float time = period;

        while (time > 0)
        {
            time -= Time.deltaTime;

            yield return null;

            if (!tree.HasResources())
            {
                Return(false);
                yield break;
            }
        }

        Return(true);
    }
}