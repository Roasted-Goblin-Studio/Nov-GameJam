using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Boss1/Boss1ProjectileAttack")]
public class Boss1ProjectileAttack : AIAttackAction
{
    protected override void AttackAction(StateController controller)
    {
        if(!_AttackStageStarted){
            _AttackStageStarted = true;
            // Start with one shot
            Debug.Log("Shooting Projectile");
            controller.Character.Weapons[1].InitiateUseWeapon();
        }
    }
}
