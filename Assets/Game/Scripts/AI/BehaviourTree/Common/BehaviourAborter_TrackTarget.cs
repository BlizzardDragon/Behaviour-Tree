using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using UnityEngine;

public class BehaviourAborter_TrackTarget : MonoBehaviour
{
    [SerializeField] private Blackboard _blackboard;
    [SerializeField] private BehaviourNode _rootNode;

    private void OnEnable()
    {
        _blackboard.OnVariableChanged += OnVariableChanged;
        _blackboard.OnVariableRemoved += OnVariableChanged;
    }

    private void OnDisable()
    {
        _blackboard.OnVariableChanged -= OnVariableChanged;
        _blackboard.OnVariableRemoved -= OnVariableChanged;

    }

    private void OnVariableChanged(string name, object value)
    {
        if(name == BlackboardKeys.TREE)
        {
            _rootNode.Abort();
        }
    }
}