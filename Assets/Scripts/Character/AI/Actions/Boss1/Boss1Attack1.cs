using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss1/Attack1")]
public class Boss1Attack1 : AIAttackAction
{
    public override void Act(StateController controller)
    {
        
        if(!AttackIsActive){
            AttackIsActive = true;
            AttackStageTell = true;

        }

        if(AttackIsActive && AttackStageTell){

        }
        
    }
}
