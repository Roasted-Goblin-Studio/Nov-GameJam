using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss1/Boss1EggDropAttack")]
public class Boss1EggDropAttack : AIAttackAction
{
    [SerializeField] protected float _NumberOfShots = 1;
    public float NumberOfShots{ get=>_NumberOfShots; set=> _NumberOfShots = value; }

    protected override void AttackAction(StateController controller)
    {
        if(!_AttackStageStarted){
            _AttackStageStarted = true;
            // Start with one shot
            Debug.Log("Shooting egg");
            controller.Character.Weapons[0].InitiateUseWeapon();
            controller.Character.Weapons[0].InitiateUseWeapon();
            controller.Character.Weapons[0].InitiateUseWeapon();
            
        }

    }
}
