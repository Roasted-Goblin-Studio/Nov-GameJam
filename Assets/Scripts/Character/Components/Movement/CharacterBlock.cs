// TODO: REMOVE ALL DEBUG LOGGING FUNCTIONS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlock : CharacterComponent
{
    // TODO: ADD CLASS VARIABLES IF NECESSARY
    
    public GameObject BlockIndicator;  // Visual indictor for debugging until we get animation
    
    private float _BlockStart = 0f;
    private float _BlockStop = 0f;
    private bool _CharacterCanBlockAgain = true;
    
    private CharacterMovement _CharacterMovement;
    

    protected override void Start()
    {
        base.Start();
        _CharacterMovement = GetComponent<CharacterMovement>();
        BlockIndicator.SetActive(false);  // Visual indictor for debugging until we get animation
    }

    protected override void HandleBasicComponentFunction(){
        DecideBlockReset();
    }

    protected override bool HandlePlayerInput()
    {
        if(!base.HandlePlayerInput()) return false;        
        if(DecideIfCharacterCanBlock())
            Block();
        
        return true;
    }

    protected override bool HandleAIInput(){
        return base.HandleAIInput();

    }

    private bool DecideIfCharacterCanBlock()
    {
        // TODO: CHECK IF BLOCK IS POSSIBLE - IS CHARACTER COLLIDING WITH AN ENEMY LAYER/TAGGED OBJECT?
        // Debug.Log($" BlockInput: {BlockInput()}  BlockCoolDownComplete: {BlockCoolDownComplete()}  canblock: {_Character.CanBlock}  BlockAgain: {_CharacterCanBlockAgain}");
        if (_Character.IsBlocking){
            return true;
        }

        if (!_Character.IsBlocking && BlockInput() && _Character.CanBlock && _CharacterCanBlockAgain && BlockCoolDownComplete()) return true;
 
        return false;
    }

    private bool BlockCoolDownComplete()
    {
        if (_BlockStop == 0f) return true;
        if (Time.time - _BlockStop >= _Character.BlockCooldownDuration) return true;
        return false;
    }

    private bool BlockInput()
    {
        if(Input.GetKey(CharacterInputs.BlockKeyCode)) return true;
        
        return false;
    }

    private void Block()
    {
        // TODO: ADD BLOCK MECHANICS
        // TODO: ADD ANIMATION CALL
        BlockIndicator.SetActive(true);
        if (!_Character.IsBlocking && _Character.CanBlock) _BlockStart = Time.time; // Unity internal timestamp for when block button pressed
        
        _Character.IsBlocking = true;
        _Character.DirectionalLocked = true;
        _CharacterCanBlockAgain = false;

        Debug.Log($" *** BLOCK FUNCTION ACTIVE *** {_BlockStart}");
    }

    private void DecideBlockReset()
    {
        // Reset if player release block button
        if(_Character.IsBlocking && Input.GetKeyUp(CharacterInputs.BlockKeyCode)){
            Debug.Log("key");
            BlockReset();
        }
        
        // Reset if player exceeds the block duration
        if(Time.time - _BlockStart > _Character.BlockDuration && _Character.IsBlocking){
            Debug.Log("duration");
            BlockReset();
        }
    }

    private void BlockReset()
    {
        // TODO: ADD BLOCK RESET LOGIC HERE
        
        BlockIndicator.SetActive(false);
        _Character.DirectionalLocked = false;
        _Character.IsBlocking = false;
        _Character.CanBlock = true;
        _CharacterCanBlockAgain = true;
        _BlockStop = Time.time;
        Debug.Log("BlockReset() " + _BlockStop);
    }

    private float TechnicalPoints()
    // This may get stripped out and put in a more central location
    // for access from multiple character actions
    {
        // TODO: BUILD LOGIC FOR TECHNICAL POINT CALCULATION (BASED ON WHEN BLOCK IS ACTIVATED AND COLLIDER CONTACTS PLAYER?)
        
        return 1f;  // placeholder value for tech points given after function completes
    }
}