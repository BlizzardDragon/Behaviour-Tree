using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;
using Tree = Sample.Tree;

public class BehavioutNodeLoop_GatheringTree : BehaviourNode, IBehaviourCallback
{
    [SerializeField] private Blackboard _blackboard;
    [SerializeField] private BehaviourNode _child;
    private Tree _tree;
    private Character _character;


    protected override void Run()
    {
        if (!_blackboard.TryGetVariable(BlackboardKeys.TREE, out Tree tree) ||
        !_blackboard.TryGetVariable(BlackboardKeys.UNIT, out Character character))
        {
            Return(false);
            return;
        }

        _tree = tree;
        _character = character;

        RunGatheringProcess();
    }

    protected override void OnAbort()
    {
        _child.Abort();
    }

    public void Invoke(BehaviourNode node, bool success)
    {
        if (success)
        {
            RunGatheringProcess();
        }
        else
        {
            Return(false);
        }
    }

    private void RunGatheringProcess()
    {
        if (!GatheringAvailable(_tree, _character))
        {
            Return(false);
            return;
        }

        _character.Chop(_tree);

        if (!GatheringAvailable(_tree, _character))
        {
            Return(false);
            return;
        }

        _child.Run(this);
    }

    private bool GatheringAvailable(Tree tree, Character character)
    {
        return tree.HasResources() && !character.IsResourceBagFull();
    }
}
