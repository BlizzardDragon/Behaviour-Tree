using System.Collections;
using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using UnityEngine;

public class BehaviourNode_WaitForSeconds : BehaviourNode
{
    [SerializeField] private string _pausetKey;
    [SerializeField] private Blackboard _blackboard;
    private Coroutine _coroutine;


    protected override void Run()
    {
        if (!_blackboard.TryGetVariable(_pausetKey, out float period))
        {
            Return(false);
            return;
        }

        _coroutine = StartCoroutine(WaitingProcess(period));
    }

    protected override void OnAbort()
    {
        StopCoroutine(_coroutine);
        Return(false);
    }

    private IEnumerator WaitingProcess(float period)
    {
        yield return new WaitForSeconds(period);
        Return(true);
    }
}