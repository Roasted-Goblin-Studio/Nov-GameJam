using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlock : CharacterComponent
{
    // TODO: ADD CLASS VARIABLES IF NECESSARY

    private CharacterMovement _CharacterMovement;

    protected override void Start()
    {
        base.Start();
        _CharacterMovement = GetComponent<CharacterMovement>();
    }

    protected override bool HandlePlayerInput(){
        if(!base.HandlePlayerInput()) return false;
        
        if(DecideIfCharacterCanBlock()) Block();

        return true;   
    }

    private bool DecideIfCharacterCanBlock(){
        // TODO: CHECK IF BLOCK IS POSSIBLE (MAYBE BASED ON IF PLAYER IS COLLIDING WITH AN ENEMY LAYER/TAGGED OBJECT?)
        if (BlockInput()) return true; // Development output with no condition checking
        return false;
    }

    private bool BlockInput(){
        if(Input.GetKeyDown(CharacterInputs.BlockKeyCode)) return true;
        return false;
    }

    private void Block(){
        // TODO: ADD BLOCK FUNCTION
        Debug.Log(" *** BLOCK FUNCTION ACTIVE *** ");
    }

    private float TechnicalPoints(){
        // TODO: BUILD LOGIC FOR TECHNICAL POINT CALCULATION (BASED ON WHEN BLOCK IS ACTIVATED AND COLLIDER CONTACTS PLAYER?)
        return 1f;  // placeholder value
    }
}