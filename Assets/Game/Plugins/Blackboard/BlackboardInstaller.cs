using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;

[RequireComponent(typeof(Blackboard))]
public class BlackboardInstaller : MonoBehaviour
{
    [SerializeField] private Character _character;
    
    
    private void Awake()
    {
        var blackboard = GetComponent<Blackboard>();
        blackboard.SetVariable(BlackboardKeys.UNIT, _character);
        blackboard.SetVariable(BlackboardKeys.BARN, FindObjectOfType<Barn>());
        blackboard.SetVariable(BlackboardKeys.TREE_STOPING_DISTANCE, 1.25f);
        blackboard.SetVariable(BlackboardKeys.BARN_STOPING_DISTANCE, 3f);
        blackboard.SetVariable(BlackboardKeys.GATHERING_PERIOD, 1f);
    }
}
