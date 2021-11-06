using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDodge : CharacterComponent
{
    [SerializeField] private LayerMask[] _LayerMasks;
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
        
        // Raycast forward to see if there are any walls or obstancles
        float dodgeDistance = _Character.IsFacingRight ? _Character.DodgeDistance : -_Character.DodgeDistance;
        float dodgeBuffer = _Character.IsFacingRight ? -0.2f : 0.2f;
        float closestLayerHit = 0; 
        foreach (LayerMask layer in _LayerMasks)
        {
            RaycastHit2D layerHit = Physics2D.Raycast(transform.position, _Character.IsFacingRight ? Vector2.right : Vector2.left, _Character.DodgeDistance, layer);
            float pointHit = layerHit.point.x;
            
            
            if(pointHit != 0){
                if(closestLayerHit == 0) closestLayerHit = pointHit + dodgeBuffer;
                else if(pointHit > 0){
                    // Keep the 0.1f in as a buffer. We want the character rigid body to fall on the left side of the collision detection.
                    if (closestLayerHit < pointHit) closestLayerHit = pointHit + dodgeBuffer;
                }
                else if(pointHit < 0){
                    // Negative values
                    if (closestLayerHit > pointHit) closestLayerHit = pointHit + dodgeBuffer;
                }
            }
        }
        
        if(closestLayerHit != 0) _EndPos = new Vector3(closestLayerHit, _StartPos.y, 0);
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
    }

}
