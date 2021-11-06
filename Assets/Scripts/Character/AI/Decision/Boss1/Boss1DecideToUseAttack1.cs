using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss1/DecideToUseAttack1")]
public class Boss1DecideToUseAttack1 : AIDecision
{
    public override Decision Decide(StateController controller)
    {
        decision.DecisionResult = true;
        return decision;
    }
}
