using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : CharacterComponent
{
    [SerializeField] private Transform _AttackPoint;
    [SerializeField] private float _AttackRange = 0.5f;
    [SerializeField] private LayerMask _EnemyLayers;
    private float _ComboFinishForce = 5.0f;
    private CharacterMovement _CharacterMovement;

    private int _MaxComboCount = 3;
    private int _CurrentComboCount = 0;
    private float _TimeUntilCharacterCanAttack = 0f;
    private float _TimeSinceLastAttack = 0f;
    private float _ComboTimeOut = 0.5f;

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
        _CharacterMovement = GetComponent<CharacterMovement>();

        if (_CharacterMovement == null) Debug.LogWarning("CharacterAttack was unable to get CharacterMovement component.");
    }

    protected override void HandleBasicComponentFunction() 
    {
        CheckAttackPointSide();
        _TimeSinceLastAttack += Time.deltaTime;
        if (_TimeSinceLastAttack >= _ComboTimeOut)
        {
            // reset combo
            _Character.IsHitStopped = false;
            _Character.IsAttacking = false;
            _CurrentComboCount = 0;
        }
    }

    private void CheckAttackPointSide()
    {
        if (_Character.IsFacingRight && _AttackPoint.localPosition.x < 0 || !_Character.IsFacingRight && _AttackPoint.localPosition.x > 0)
        {
            //Debug.Log("Flipping... " + _AttackPoint.position.x + " inverted: " + _AttackPoint.position.x * -1);
            _AttackPoint.localPosition = new Vector2((_AttackPoint.localPosition.x * -1), _AttackPoint.localPosition.y);
        }
    }

    protected override bool HandlePlayerInput()
    {
        if (!base.HandlePlayerInput()) return false;

        if (DecideIfCharacterCanAttack()) Attack();

        return true;
    }

    protected override bool HandleAIInput()
    {
        return base.HandleAIInput();

    }

    private void Attack()
    {
        var attackLength = 0.3f; // hard coding for testing
        if (_Character.IsGrounded) _CharacterMovement.SetMovementLockTime(attackLength);
        _TimeSinceLastAttack = 0f;
        _CurrentComboCount++;
        _Character.IsAttacking = true;


        _Animation.ChangeAnimationState(string.Format("Attack{0}", _CurrentComboCount), CharacterAnimation.AnimationType.Static);

        if (_CurrentComboCount == _MaxComboCount)
        {
            // apply upward force on combo finisher
            _Character.RigidBody2D.velocity = Vector2.up * _ComboFinishForce;
            _CurrentComboCount = 0;

            attackLength += 0.2f; // hard coded for testing
        }

        _TimeUntilCharacterCanAttack = Time.time + attackLength;

        if (!_AttackPoint) return;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_AttackPoint.position, _AttackRange, _EnemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            _Character.IsHitStopped = true;
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
        return AttackInput() && Time.time >= _TimeUntilCharacterCanAttack;
    }

    private bool AttackInput()
    {
        return (Input.GetKeyDown(CharacterInputs.AttackKeyCode));
    }
}