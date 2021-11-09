using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponent
{
    // Private
    private float _HorizontalMovement;
    private float _VerticalMovement = 0f;
    private float _HorizontalForceApplied;
    private float _VerticalForceApplied;
    private float _MovementCompoundValue = 0.015f;
    private float _HorizontalEnviromentalForceApplied = 0;
    private float _VerticalEnviromentalForceApplied = 0;
    private float _MovementLockTimer = 0f;

    // Protected

    // Serialized
    [SerializeField] private float _MovementSpeed;
    [SerializeField] private bool _UsesVerticalMovement = false;
    [SerializeField] private float _MaxSpeed = 100f;
    [SerializeField] private float _DragToBeApplied = 3f;

    // Public
    public float HorizontalMovement { get => _HorizontalMovement; set => _HorizontalMovement = value; }
    public float VerticalMovement { get => _VerticalMovement; set => _VerticalMovement = value; }
    public float DragToBeApplied { get => _DragToBeApplied; set => _DragToBeApplied = value; }
    public float HorizontalEnviromentalForceApplied { get => _HorizontalEnviromentalForceApplied; set => _HorizontalEnviromentalForceApplied = value; }
    public float VerticalEnviromentalForceApplied { get => _VerticalEnviromentalForceApplied; set => _VerticalEnviromentalForceApplied = value; }


    protected override void Start(){
        base.Start();
        SetLayerCollisionIgnores();
        _Character.RigidBody2D.drag = DragToBeApplied;

        if(!_Character.CharacterUsesGravity) {
            _Character.RigidBody2D.gravityScale = 0;
        }
    }

    protected override void Update()
    {
        base.Update();
        DetectIfGrounded();
        DetectIfMovementLocked();
    }

    public void DetectIfMovementLocked()
    {
        if (Time.time > _MovementLockTimer) _Character.MovementLocked = false;
    }

    public void SetMovementLockTime(float time)
    {
        if (_MovementLockTimer > Time.time + time) return;
        _Character.MovementLocked = true;
        _MovementLockTimer = Time.time + time;
    }

    protected override void HandlePhysicsComponentFunction(){
        base.HandlePhysicsComponentFunction();
        if(_Character.DirectionalLocked) return;

        // Horizontal
        if(!_Character.MovementLocked){
            _HorizontalForceApplied = _MovementSpeed * _HorizontalMovement;
            if(_HorizontalForceApplied > _MaxSpeed) _HorizontalForceApplied = _MaxSpeed;
            else if(_HorizontalForceApplied < -_MaxSpeed) _HorizontalForceApplied = -_MaxSpeed;
        
            // if the character is facing right, the wind should blow them left
            // if the chracter is facing left, the wind should blow them right
            if(_Character.IsInWind){
                if(_Character.IsFacingRight){
                    if(_HorizontalMovement == 0){
                        _HorizontalForceApplied = HorizontalEnviromentalForceApplied;
                    }
                    else{
                        _HorizontalForceApplied += HorizontalEnviromentalForceApplied;
                    }

                }else{
                    if(_HorizontalMovement == 0){
                        _HorizontalForceApplied = HorizontalEnviromentalForceApplied;
                    }
                    else{
                        _HorizontalForceApplied += HorizontalEnviromentalForceApplied;
                    }
                }
            }

            // Vertical 
            if(_UsesVerticalMovement && !_Character.MovementLocked){
                _VerticalForceApplied = _MovementSpeed * _VerticalMovement;
                 if(_VerticalForceApplied > _MaxSpeed) _VerticalForceApplied = _MaxSpeed;
                 else if(_VerticalForceApplied < -_MaxSpeed) _VerticalForceApplied = -_MaxSpeed;    
            }
            
            // I'm fairly confident that the wind effect won't work with the vertical movement style ... yet
            float verticalMovement = _VerticalForceApplied + VerticalEnviromentalForceApplied;

            _Character.RigidBody2D.AddForce(new Vector2(_HorizontalForceApplied, verticalMovement), ForceMode2D.Impulse); // <-- Immediate force applied
        }
    }

    protected override bool HandlePlayerInput(){
        if(!base.HandlePlayerInput()) return false;

        CalcPlayerHorizontalInputs();
        CalcPlayerVerticalInputs();

        if(!_Character.MovementLocked){
            _Character.IsMoving = _HorizontalMovement != 0;
            
            if(_Character.IsMoving){
                if(_Character.IsFacingRight && _HorizontalMovement < 0 || !_Character.IsFacingRight && _HorizontalMovement > 0){
                    FlipCharacter();
                }
            }
        }
        return true;
    }

    protected override bool HandleAIInput(){
        if(!base.HandleAIInput()) return false;
        // Movement values are handled through the public SetFunctions

        CalcAIHorizontalInputs();
        CalcAIVerticalInputs();

        if(!_Character.MovementLocked){
            _Character.IsMoving = _HorizontalMovement != 0;
            if(_Character.IsMoving){
                if(_Character.IsFacingRight && _HorizontalMovement < 0 || !_Character.IsFacingRight && _HorizontalMovement > 0){
                    //FlipCharacter();
                }

                // Character is moving.
                // Animation logic to be added here.
            }
        }
        return true;
    }

    private void FlipCharacter(){
        var character = _Character.Sprite.transform;
        _Character.IsFacingRight = !_Character.IsFacingRight;
        character.localRotation = Quaternion.Euler(character.rotation.x, _Character.IsFacingRight ? 0 : -180, character.rotation.z);
    }

    private void SetLayerCollisionIgnores(){
        // Ignore Layer collision would go in here.
        /* Sample of the old version.. 
        if (_Character.CharacterType == Character.CharacterTypes.Player){ Physics2D.IgnoreLayerCollision(9, 9); // <--  ignore collision with "Dead Bodies" while rolling
        }

        else if (_Character.CharacterType == Character.CharacterTypes.AI){Physics2D.IgnoreLayerCollision(8, 8);
        }
        */
    }

    public void MovePosition(Vector2 newPos){
        _Character.RigidBody2D.MovePosition(newPos);
    }

    private void DetectIfGrounded(){
        if(!_Character.GroundSensor) return;
        if(_Character.GroundSensor.SensorActivated) _Character.IsGrounded = true;
        else _Character.IsGrounded = false;
    }

    private void CalcPlayerHorizontalInputs(){
        // It's suggested online that KeyCodes are used over Input.RawAxis
        // This allows us to retain control over the Keybindings in the CharacterInput class.
        if(Input.GetKey(CharacterInputs.MovementLeftKeyCode) && Input.GetKey(CharacterInputs.MovementRightKeyCode)){
            _HorizontalMovement = 0;
        }
        else if(Input.GetKey(CharacterInputs.MovementLeftKeyCode)){
            if(_HorizontalMovement > -.9f){
                _HorizontalMovement += -_MovementCompoundValue;
            }
        }else if(Input.GetKey(CharacterInputs.MovementRightKeyCode)){
            if(_HorizontalMovement < .9f){ // .9 Has a really good feel to it with the current player values. 
                _HorizontalMovement += _MovementCompoundValue;
            }
        } 
        else{
            _HorizontalMovement = 0;
            // _Character.CharacterRigidBody2D.velocity = new Vector2(0,0);
        }
    }

    private void CalcAIHorizontalInputs(){
        // It's suggested online that KeyCodes are used over Input.RawAxis
        // This allows us to retain control over the Keybindings in the CharacterInput class.
        if(Input.GetKey(CharacterInputs.AIMovementLeftKeyCode) && Input.GetKey(CharacterInputs.AIMovementRightKeyCode)){
            _HorizontalMovement = 0;
        }
        else if(Input.GetKey(CharacterInputs.AIMovementLeftKeyCode)){
            if(_HorizontalMovement > -.9f){
                _HorizontalMovement += -_MovementCompoundValue;
            }
        }else if(Input.GetKey(CharacterInputs.AIMovementRightKeyCode)){
            if(_HorizontalMovement < .9f){ // .9 Has a really good feel to it with the current player values. 
                _HorizontalMovement += _MovementCompoundValue;
            }
        } 
        else{
            _HorizontalMovement = 0;
            // _Character.CharacterRigidBody2D.velocity = new Vector2(0,0);
        }
    }

    private void CalcPlayerVerticalInputs(){
        if(!_UsesVerticalMovement){return;}
        // TODO ^^ Based on the above Horizontal.
    }

    private void CalcAIVerticalInputs(){
        if(!_UsesVerticalMovement){return;}
        if(Input.GetKey(CharacterInputs.AIMovementDownKeyCode) && Input.GetKey(CharacterInputs.AIMovementUpKeyCode)){
            _VerticalMovement = 0;
        }
        else if(Input.GetKey(CharacterInputs.AIMovementDownKeyCode)){
            if(_VerticalMovement > -.9f){
                _VerticalMovement += -_MovementCompoundValue;
            }
        }else if(Input.GetKey(CharacterInputs.AIMovementUpKeyCode)){
            if(_VerticalMovement < .9f){ // .9 Has a really good feel to it with the current player values. 
                _VerticalMovement += _MovementCompoundValue;
            }
        } 
        else{
            _VerticalMovement = 0;
            // _Character.CharacterRigidBody2D.velocity = new Vector2(0,0);
        }
    }
}
