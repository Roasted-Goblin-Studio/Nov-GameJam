using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AIAttackAction : AIAction
{
    [SerializeField] protected string _AttackName;
    // General
    protected float AttackTimer = 0;
    protected bool AttackIsActive = false;

    // Tell
    [SerializeField] protected float _TimeOfTell=0; // The time from start to finish for the tell to take place
    protected bool AttackStageTell = false;
    
    // Attack
    [SerializeField] protected float _TimeOfAttack=0;
    protected bool AttackStageAttack = false;
    
    // Outro
    [SerializeField] protected float _TimeOfEnd = 0;
    protected bool AttackStageEnd = false;  

    public float TimeOfTell { get => _TimeOfTell; set => _TimeOfTell = value; }
    public float TimeOfAttack { get => _TimeOfAttack; set => _TimeOfAttack = value; }
    public float TimeOfEnd { get => _TimeOfEnd; set => _TimeOfEnd = value; }


    protected void OnDisable(){
        AttackTimer = 0;
        AttackIsActive = false;
        AttackStageTell = false;
        AttackStageAttack = false;
        AttackStageEnd = false;
    }
}
