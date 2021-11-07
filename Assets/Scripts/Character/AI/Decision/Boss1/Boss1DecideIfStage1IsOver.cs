using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss1/DecideIfStage1IsOver")]
public class Boss1DecideIfStage1IsOver : AIDecision
{
    public override Decision Decide(StateController controller)
    {
        if(controller.Character.CharacterHealth.CurrentHealth == 0){
            decision.Priority = SetPriority;
            decision.DecisionResult = true;
        }
        return decision;
    }
}
