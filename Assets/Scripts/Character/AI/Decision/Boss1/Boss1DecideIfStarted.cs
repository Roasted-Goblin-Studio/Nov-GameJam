using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/DecideIfStarted")]
public class Boss1DecideIfStarted : AIDecision
{
    public override Decision Decide(StateController controller)
    {        
        if(controller.AIFlags.BossHasStarted){
            decision.DecisionResult = true;
        }
    
        return decision;
    }
}
