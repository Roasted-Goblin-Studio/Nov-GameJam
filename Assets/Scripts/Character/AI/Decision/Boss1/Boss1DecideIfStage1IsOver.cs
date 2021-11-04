using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/DecideIfStage1IsOver")]

public class Boss1DecideIfStage1IsOver : AIDecision
{
    public override Decision Decide(StateController controller)
    {
        Debug.Log("Check?");
        if(controller.Character.CharacterHealth.CurrentHealth == 0){
            decision.Priority  = 10;
            decision.DecisionResult = true;
        }
        return decision;
    }
}
