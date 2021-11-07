using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputs : MonoBehaviour
{
    // Jumping
    [SerializeField] private KeyCode _JumpKeyCode = KeyCode.Space; 
    public KeyCode JumpKeyCode {get => _JumpKeyCode; set => _JumpKeyCode = value;}
    
    // Pausing
    [SerializeField] private KeyCode _PauseKeyCode = KeyCode.F;
    public KeyCode PauseKeyCode {get => _PauseKeyCode; set => _PauseKeyCode = value;}

    // Dodging
    [SerializeField] private KeyCode _DodgeKeyCode = KeyCode.LeftShift;
    public KeyCode DodgeKeyCode {get => _DodgeKeyCode; set => _DodgeKeyCode = value;}

    // Attacking 
    [SerializeField] private KeyCode _AttackKeyCode = KeyCode.Mouse0;
    public KeyCode AttackKeyCode {get => _AttackKeyCode; set => _AttackKeyCode = value;}

    // Blocking
    [SerializeField] private KeyCode _BlockKeyCode = KeyCode.E;
    public KeyCode BlockKeyCode {get => _BlockKeyCode; set => _BlockKeyCode = value;}

    // Movement
    //[SerializeField] private KeyCode _MovementCode;
    // public Input MovementCode {get => _MovementCode; set => _MovementCode = value;}
    [SerializeField] private KeyCode _MovementLeftKeyCode = KeyCode.A;
    public KeyCode MovementLeftKeyCode {get => _MovementLeftKeyCode; set => _MovementLeftKeyCode = value;}
    [SerializeField] private KeyCode _MovementRightKeyCode = KeyCode.D;
    public KeyCode MovementRightKeyCode {get => _MovementRightKeyCode; set => _MovementRightKeyCode = value;}

    // Use Weapon
    [SerializeField] private KeyCode _WeaponKeyCode = KeyCode.J;
    public KeyCode WeaponKeyCode {get => _WeaponKeyCode; set => _WeaponKeyCode = value;}
    [SerializeField] private KeyCode _WeaponReloadKeyCode = KeyCode.R;
    public KeyCode WeaponReloadKeyCode {get => _WeaponReloadKeyCode; set => _WeaponReloadKeyCode = value;}

    // AI TESTING
    [SerializeField] private KeyCode _AIMovementLeftKeyCode = KeyCode.LeftArrow;
    public KeyCode AIMovementLeftKeyCode {get => _AIMovementLeftKeyCode; set => _AIMovementLeftKeyCode = value;}
    [SerializeField] private KeyCode _AIMovementRightKeyCode = KeyCode.RightArrow;
    public KeyCode AIMovementRightKeyCode {get => _AIMovementRightKeyCode; set => _AIMovementRightKeyCode = value;}
    [SerializeField] private KeyCode _AIMovementUpKeyCode = KeyCode.UpArrow;
    public KeyCode AIMovementUpKeyCode {get => _AIMovementUpKeyCode; set => _AIMovementUpKeyCode = value;}
    [SerializeField] private KeyCode _AIMovementDownKeyCode = KeyCode.DownArrow;
    public KeyCode AIMovementDownKeyCode {get => _AIMovementDownKeyCode; set => _AIMovementDownKeyCode = value;}
}
