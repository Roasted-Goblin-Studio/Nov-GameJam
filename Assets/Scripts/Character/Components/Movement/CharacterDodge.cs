using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDodge : CharacterComponent
{
    private Vector3 _StartPos;
    private Vector3 _EndPos;
    private float _Fraction = 0f;
    private bool _CharacterCanDodgeAgain = false;

    private CharacterMovement _CharacterMovement;

    protected override void Start()
    {
        base.Start();
        _CharacterMovement = GetComponent<CharacterMovement>();
    }

    protected override void HandlePhysicsComponentFunction(){
        CalculateDodge();
        CalculateDodgeReset();
    }

    protected override bool HandlePlayerInput(){
        if(!base.HandlePlayerInput()) return false;
        
        if(DecideIfCharacterCanDodge()) Dodge();

        return true;   
    }

    protected override bool HandleAIInput(){
        return base.HandleAIInput();

    }

    private bool DecideIfCharacterCanDodge(){        
        if(DodgeInput() && _Character.CanDodge && _CharacterCanDodgeAgain) return true;
        return false;
    }

    private bool DodgeInput(){
        if(Input.GetKeyDown(CharacterInputs.DodgeKeyCode)) return true;
        return false;
    }

    private void Dodge(){
        _Animation.ChangeAnimationState("Dash", CharacterAnimation.AnimationType.Static);
        _Character.IsDodging = true;
        _Character.CanDodge = false;
        _Character.DirectionalLocked = true;
        _CharacterCanDodgeAgain = false;
        _CharacterMovement.HorizontalMovement = 0;
        
        _StartPos = transform.position;
        // Raycast forward to see if there are any walls or obstancles?
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, _Character.IsFacingRight ? Vector2.right : Vector2.left, _Character.DodgeDistance, (1 << LayerMask.GetMask("Walls") | 1 << LayerMask.GetMask("Platforms")));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _Character.IsFacingRight ? Vector2.right : Vector2.left, _Character.DodgeDistance, LayerMask.GetMask("Walls"));
        float dodgeDistance = _Character.IsFacingRight ? _Character.DodgeDistance : -_Character.DodgeDistance;
        
        if(hit.collider != null) _EndPos = new Vector3(hit.point.x, _StartPos.y, 0);
        else _EndPos = new Vector3(_StartPos.x + dodgeDistance, _StartPos.y, 0);
    }

    private void CalculateDodge(){
        if (_Character.IsDodging)
        {
            if(_Fraction < 1){
                _Fraction += (_Character.DodgeDistance * _Character.DodgeSpeed);
                transform.position = Vector3.Lerp(_StartPos, _EndPos, _Fraction);
            }else{
                _Character.DirectionalLocked = false;
                _Character.IsDodging = false;
                _Character.CanDodge = true;
                _Fraction = 0f;
            }
        }
    }

    private void CalculateDodgeReset(){
        if(_Character.IsGrounded) _CharacterCanDodgeAgain = true;
        if(_Character.SlidingSensorL1.SensorActivated || _Character.SlidingSensorR1.SensorActivated) _CharacterCanDodgeAgain = true;
        // HEY! LISTEN:
        // MuLtIpLe JuMp FlAgS cAn Be SeT hErE
    }

}
