using System.Collections;
using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;

public class BehaviourNode_MoveToPosition : BehaviourNode
{
    [SerializeField] private Blackboard _blackboard;
    private Coroutine _coroutine;


    protected override void Run()
    {
        if (!_blackboard.TryGetVariable(BlackboardKeys.MOVE_POSITION, out Vector3 movePosition) ||
        !_blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character character) ||
        !_blackboard.TryGetVariable(BlackboardKeys.STOPING_DISTANCE, out float stopingDistance))
        {
            Return(false);
            return;
        }

        _coroutine = StartCoroutine(MoveCharacter(movePosition, character, stopingDistance));
    }

    protected override void OnAbort()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator MoveCharacter(Vector3 movePosition, Character character, float stopingDistance)
    {
        var characterTransform = character.transform;

        Vector3 characterPosition;
        Vector3 distanceVector;
        Vector3 moveDirection;
        float distance;

        while (true)
        {
            characterPosition = characterTransform.position;
            distanceVector = movePosition - characterPosition;

            distance = distanceVector.magnitude;
            if (distance <= stopingDistance)
            {
                break;
            }

            moveDirection = distanceVector.normalized;

            character.Move(moveDirection);

            yield return null;
        }

        Return(true);
    }
}