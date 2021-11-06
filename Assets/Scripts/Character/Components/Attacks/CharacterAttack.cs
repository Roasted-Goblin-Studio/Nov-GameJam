using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack: CharacterComponent
{
    private LayerMask _EnemyLayers;
    private float _AttackRange = 0.5f;
    private Transform _AttackPoint;
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void HandleBasicComponentFunction() { }

    protected override bool HandlePlayerInput()
    {
        if(!base.HandlePlayerInput()) return false;        
        return true;
    }

    protected override bool HandleAIInput()
    {
        return base.HandleAIInput();

    }

    private bool DecideIfCharacterCanAttack()
    {
        return false;
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_AttackPoint.position, _AttackRange, _EnemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!_AttackPoint) return;

        Gizmos.DrawWireSphere(_AttackPoint.position, _AttackRange);
    }

    private float TechnicalPoints()
    // This may get stripped out and put in a more central location
    // for access from multiple character actions
    {
        // TODO: BUILD LOGIC FOR TECHNICAL POINT CALCULATION (BASED ON WHEN BLOCK IS ACTIVATED AND COLLIDER CONTACTS PLAYER?)
        
        return 1f;  // placeholder value for tech points given after function completes
    }
}