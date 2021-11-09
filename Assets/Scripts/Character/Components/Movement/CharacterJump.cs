using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : CharacterComponent
{
    // private
    private bool _CharacterIsJumping = false;
    private float _TimeSinceLastJump = 0f;
    private float _LowJumpModifier = 2.5f;
    private float _JumpStartPos;
    private int _MaxJumps = 2;
    private int _JumpsRemaining;
    // private CharacterMovement _CharacterMovement;

    // Serialized
    [SerializeField] private float _FallMultiplier = 2.5f;
    [SerializeField] private float _GravityScaled = 1f;
    [SerializeField] private float _VerticalTakeOff = 15f;
    [SerializeField] private float _TimeBetweenJumps;
    [SerializeField] private float _WallJumpPushOff = 1000f;

    // TESTING
    [SerializeField] private bool WallJumpType2 = true;


    // public
    public float FallMultiplier { get => _FallMultiplier; set => _FallMultiplier = value; }
    public float GravityScaled { get => _GravityScaled; set => _GravityScaled = value; }
    public float VerticalTakeOff { get => _VerticalTakeOff; set => _VerticalTakeOff = value; }
    public float TimeBetweenJumps { get => _TimeBetweenJumps; set => _TimeBetweenJumps = value; }
    public float LowJumpModifier { get => _LowJumpModifier; set => _LowJumpModifier = value; }
    public bool CharacterIsJumping { get => _CharacterIsJumping; set => _CharacterIsJumping = value; }

    protected override void Start()
    {
        base.Start();
        // _CharacterMovement = GetComponent<CharacterMovement>();
        _JumpsRemaining = _MaxJumps;
    }

    protected override void HandleBasicComponentFunction()
    {
        DecideIfCharacterLanded();
        CalculateComponentData();
    }

    protected override void HandlePhysicsComponentFunction()
    {
        ApplyGravity();
    }

    protected override bool HandlePlayerInput()
    {
        if (!base.HandlePlayerInput()) return false;

        //if (DecideIfCharacterCanJump()) StartCoroutine(JumpCoroutine());
        if (DecideIfCharacterCanJump()) Jump();
        if (DecideIfCharacterCanWallJump()) WallJump();

        return true;
    }

    protected override bool HandleAIInput()
    {
        // TODO: AI JUMP
        if (!base.HandleAIInput()) return false;
        return false;
    }

    private bool DecideIfCharacterCanJump()
    {
        // TODO: JUMP TIMEOUT
        // TODO: Add lockouts
        // check for double jump
        return (_Character.GroundSensor.SensorActivated || _JumpsRemaining > 0) && JumpInput();
    }
    
    private bool DecideIfCharacterCanWallJump()
    {
        // TODO: JUMP TIMEOUT
        // TODO: Add lockouts
        return _Character.CanWallSlideJump && JumpInput();
    }
    private bool JumpInput()
    {
        return Input.GetKeyDown(CharacterInputs.JumpKeyCode);
    }

    private void Jump()
    {
        _Animation.ChangeAnimationState(string.Format("JumpStart{0}", _JumpsRemaining == _MaxJumps ? "" : "Air"), CharacterAnimation.AnimationType.Static);
        _JumpStartPos = transform.position.y;
        _Character.RigidBody2D.velocity = Vector2.up * VerticalTakeOff;
        CharacterIsJumping = true;
        _JumpsRemaining--;
    }

    // the jump animation has a buildup delay and requires the animation to play for 2 frames before jumping
    private IEnumerator JumpCoroutine()
    {
        _Animation.ChangeAnimationState("JumpStart", CharacterAnimation.AnimationType.Static);
        CharacterIsJumping = true;
        _JumpsRemaining--;
        _JumpStartPos = transform.position.y;
        yield return new WaitForSeconds(0.1f);
        _Character.RigidBody2D.velocity = Vector2.up * VerticalTakeOff;
    }
    
    private void WallJump()
    {
        _JumpStartPos = transform.position.y;
        _Character.RigidBody2D.velocity = Vector2.up * VerticalTakeOff;
        
        float wallJumpVertical = WallJumpType2 ? _WallJumpPushOff : 0;
        float wallJumpPushOff =  !_Character.IsFacingRight ? _WallJumpPushOff : -_WallJumpPushOff;
        _Character.RigidBody2D.AddForce(new Vector2(wallJumpPushOff, wallJumpVertical), ForceMode2D.Force);
        _JumpsRemaining = _MaxJumps - 1;
        CharacterIsJumping = true;
    }

    private void ApplyGravity()
    {
        // does it make sense to keep gravity calculation
        if (_Character.IsAttacking) _GravityScaled = 0.3f;
        else if (_Character.IsHitStopped) _GravityScaled = 0f;
        else _GravityScaled = 1f;

        _Character.RigidBody2D.gravityScale = _GravityScaled;

        if (_Character.RigidBody2D.velocity.y < 0f)
        {
            _Character.IsFalling = true;
            // Effects rigidbody with downward force
            _Character.RigidBody2D.velocity += new Vector2(0, (1 * Physics2D.gravity.y * FallMultiplier * Time.deltaTime) * GravityScaled);
        }
        else if (_Character.RigidBody2D.velocity.y > 0f)
        {
            // Creates "Video game" jump that has a snappier up and floaty down
            _Character.RigidBody2D.velocity += new Vector2(0, (1 * Physics2D.gravity.y * LowJumpModifier * Time.deltaTime) * GravityScaled);
        }
    }

    private void DecideIfCharacterLanded()
    {
        if (_Character.GroundSensor.SensorActivated && _Character.RigidBody2D.velocity.y <= 0f && _Character.IsFalling)
        {
            // TODO: Lots of things can be done here, but one that I think I may be interested in looking into is fall damage
            CharacterIsJumping = false;
            _JumpsRemaining = _MaxJumps;
            _Animation.ChangeAnimationState("Land", CharacterAnimation.AnimationType.Static);
            _Character.IsFalling = false;
        }
    }

    private void CalculateComponentData()
    { // TODO: MAYBE MOVE THIS TO THE COMPONENT CLASS
        if (!IsPlayer()) return;
        float curJumpHeight = transform.position.y;
        if (curJumpHeight > CharacterAchievements.HighestJumpedReached && _CharacterIsJumping)
        {
            CharacterAchievements.HighestJumpedReached = curJumpHeight;
        }
    }
}
