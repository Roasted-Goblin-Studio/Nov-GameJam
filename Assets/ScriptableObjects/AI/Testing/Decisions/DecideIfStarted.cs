using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/DecideIfStarted")]
public class DecideIfStarted : AIDecision
{
    public override Decision Decide(StateController controller)
    {
        Decision decision = new Decision();
        
        if(controller.AIFlags.BossHasStarted){
            decision.DecisionResult = true;
        }
    
        return decision;
    }
}
