using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss1/Attack1")]
public class Boss1Attack1 : AIAttackAction
{
    public override void Act(StateController controller)
    {
        
        if(!_AttackIsActive){
            _AttackIsActive = true;
            _AttackStageTell = true;

        }

        if(_AttackIsActive && _AttackStageTell){

        }
        
    }
}
