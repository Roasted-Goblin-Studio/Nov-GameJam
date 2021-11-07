using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss1/DecideIfProjectileAttack")]
public class Boss1DecideIfProjectileAttack : AIDecision
{
    public override Decision Decide(StateController controller)
    {
        // Lowest level decision
        decision.DecisionResult = true;
        return decision;
    }
}
