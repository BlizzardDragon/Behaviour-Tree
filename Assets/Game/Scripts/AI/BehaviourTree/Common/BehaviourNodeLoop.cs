using System.Collections;
using Lessons.AI.LessonBehaviourTree;
using UnityEngine;

public class BehaviourNodeLoop : BehaviourNode, IBehaviourCallback
{
    [SerializeField] private BehaviourNode _child;

    protected override void Run()
    {
        _child.Run(callback: this);
    }

    public void Invoke(BehaviourNode node, bool success)
    {
        if (success)
        {
            StartCoroutine(Loop());
        }
        else
        {
            Return(false);
        }
    }

    private IEnumerator Loop()
    {
        yield return null;
        _child.Run(callback: this);
    }
}