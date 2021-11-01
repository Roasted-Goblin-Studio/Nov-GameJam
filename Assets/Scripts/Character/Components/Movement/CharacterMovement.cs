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
    }

    protected override void Update()
    {
        base.Update();
        DetectIfGrounded();
    }

    protected override void HandlePhysicsComponentFunction(){
        base.HandlePhysicsComponentFunction();
        if(_Character.DirectionalLocked) return;

        // Horizontal
        if(!_Character.MovementLocked && !_UsesVerticalMovement){
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

            float verticalMovement = VerticalMovement + VerticalEnviromentalForceApplied;
            Debug.Log("Vertical Movement: " + verticalMovement);

            _Character.RigidBody2D.AddForce(new Vector2(_HorizontalForceApplied, verticalMovement), ForceMode2D.Impulse); // <-- Immediate force applied
        }

        // Vertical 
        if(_UsesVerticalMovement && !_Character.MovementLocked){
            var calch = _MovementSpeed * _HorizontalMovement;
            var calcv = _MovementSpeed * _VerticalMovement;

            if(calch > _MaxSpeed) calch = _MaxSpeed;
            else if(calch < -_MaxSpeed) calch = -_MaxSpeed;

            if(calcv > _MaxSpeed) calcv = _MaxSpeed;
            else if(calcv < -_MaxSpeed) calcv = -_MaxSpeed;

            _Character.RigidBody2D.AddForce(new Vector2(calch, calcv), ForceMode2D.Force);        
        }
    }

    protected override bool HandlePlayerInput(){
        if(!base.HandlePlayerInput()) return false;

        //_HorizontalMovement = Input.GetAxis("Horizontal");
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

    private void CalcPlayerVerticalInputs(){
        if(!_UsesVerticalMovement){return;}
        // TODO ^^ Based on the above Horizontal.
    }
}
