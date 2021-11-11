using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack: CharacterComponent
{
    private LayerMask _EnemyLayers;
    private float _AttackRange = 0.5f;
    private Transform _AttackPoint;


    private bool _IsAttacking = false;
    private int _MaxComboCount = 3;
    private int _CurrentComboCount = 0;
    private float _TimeUntilCharacterCanAttack = 0f;

    // We need to track:
    // - attack delays (i.e, when the player has to wait before making an attack. 
    // - time until combo resets
    // - total attacks in combo
    // - current combo count
    // each attack should have:
    //   - damage
    //   - range?
    //   - tech point value
    //   - end lag value
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void HandleBasicComponentFunction() { }

    protected override bool HandlePlayerInput()
    {
        Debug.Log("attack");
        if (!base.HandlePlayerInput() || GlobalStateManager.GameIsPaused) return false;

        if (DecideIfCharacterCanAttack()) Attack();

        return true;
    }

    protected override bool HandleAIInput()
    {
        return base.HandleAIInput();

    }

    private void Attack()
    {
        if (_CurrentComboCount == _MaxComboCount)
        {
            _CurrentComboCount = 0;
        } 
        else
        {
            _CurrentComboCount++;
        }
        //_TimeUntilCharacterCanAttack = Time.time + 0.3333f;
        _Animation.ChangeAnimationState(string.Format("Attack{0}", _CurrentComboCount), CharacterAnimation.AnimationType.Static);

        if (!_AttackPoint) return;
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

    private bool DecideIfCharacterCanAttack()
    {
        return AttackInput();// && Time.time <= _TimeUntilCharacterCanAttack;
    }

    private bool AttackInput()
    {
        return (Input.GetKeyDown(CharacterInputs.AttackKeyCode));
    }
}