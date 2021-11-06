using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Boss1/DecideIfIntroFinished")]
public class Boss1DecideIfIntroIsDone : AIDecision
{
    public override Decision Decide(StateController controller)
    {
        if(controller.AIFlags.BossIntroHasFinished){
            decision.DecisionResult = true;
        }

        return decision;
    }

    // private void OnEnable() {
    //     Debug.Log("Enabled");
    // }
}
