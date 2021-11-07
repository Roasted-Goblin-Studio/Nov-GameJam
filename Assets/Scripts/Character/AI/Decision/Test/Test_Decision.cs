using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Test_Decision")]
public class Test_Decision : AIDecision
{
    // [SerializeField] protected bool _TestSimpleDecision = false;
    // [Range(1,10)]
    // [SerializeField] protected int _Priority = 1;

    public override Decision Decide(StateController controller)
     {
    //     Decision decision = new Decision();

    //      decision.DecisionResult = _TestSimpleDecision;
    //      decision.Priority = _Priority;

        return decision;
     }
}
