using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AIAttackAction : AIAction
{
    [SerializeField] protected string _AttackName;
    // General
    protected float _AttackTimer = 0;
    protected bool _AttackIsActive = false;

    // Tell
    [SerializeField] protected float _TimeOfTell=0; // The time from start to finish for the tell to take place
    protected bool _AttackStageTell = false;
    protected bool _TellStageStarted = false;
    
    // Attack
    [SerializeField] protected float _TimeOfAttack=0;
    protected bool _AttackStageAttack = false;
    protected bool _AttackStageStarted = false;
    
    // Outro
    [SerializeField] protected float _TimeOfEnd = 0;
    protected bool _AttackStageEnd = false;
    protected bool _EndStageStarted = false; 

    // Cooldown
    [SerializeField] protected float _TimeOfCooldown = 0;
    protected bool _CanAttackAgain = true; 
    protected bool _AttackCoolDownActive = false;

    // Timers
    protected float _AttackStartTime = 0;
    protected float _AttackTellTimeWhenDone = 0;
    protected float _AttackTimeWhenDone = 0;
    protected float _AttackEndTimeWhenDone = 0;
    protected float _AttackCoolDown = 0;
    

    public float TimeOfTell { get => _TimeOfTell; set => _TimeOfTell = value; }
    public float TimeOfAttack { get => _TimeOfAttack; set => _TimeOfAttack = value; }
    public float TimeOfEnd { get => _TimeOfEnd; set => _TimeOfEnd = value; }

    public override void Act(StateController controller)
    {
        if(_CanAttackAgain && !_AttackIsActive){
            _CanAttackAgain = false;
            _AttackIsActive = true;
            _AttackStageTell = true;
            _AttackStartTime = Time.time;
            _AttackTellTimeWhenDone = _AttackStartTime + TimeOfTell;
        }

        if(_AttackIsActive && _AttackStageTell){
            // Triggers tell
            Debug.Log("Tell Stage Active");

            TellAction();

            if(Time.time > _AttackTellTimeWhenDone){
                _AttackStageTell = false;
                _TellStageStarted = false;
                _AttackStageAttack = true;
                _AttackTimeWhenDone = _AttackTellTimeWhenDone + TimeOfAttack;
            }
        }

        if(_AttackIsActive && _AttackStageAttack){
            Debug.Log("Attack Stage Active");
            
            AttackAction(controller);

            if(Time.time > _AttackTimeWhenDone){
                _AttackStageAttack = false;
                _AttackStageStarted = false;
                _AttackStageEnd = true;
                _AttackEndTimeWhenDone = _AttackTimeWhenDone + TimeOfEnd;
            }
        }

        if(_AttackIsActive && _AttackStageEnd){
            Debug.Log("End Stage Active");

            EndAction();

            if(Time.time > _AttackEndTimeWhenDone){
                _AttackStageEnd = false;
                _EndStageStarted = false;
                _AttackIsActive = false;
                _AttackCoolDown = Time.time + _TimeOfCooldown;
                _AttackCoolDownActive = true;
            }
        }

        if(_AttackCoolDownActive) Debug.Log("Cooldown Active");

        if(_AttackCoolDownActive && Time.time > _AttackCoolDown) {
            Debug.Log("Cooldown Finished");
            _AttackCoolDownActive = false;
            _CanAttackAgain = true;
        }
    }

    protected virtual void TellAction(){
        if(!_TellStageStarted){
            _TellStageStarted = true;
            Debug.Log("Tell Action");
        }
    }

    protected virtual void AttackAction(StateController controller){
        if(!_AttackStageStarted){
            _AttackStageStarted = true;
            Debug.Log("Attack Action");
        }
    }

    protected virtual void EndAction(){
        if(!_EndStageStarted){
            _EndStageStarted = true;
            Debug.Log("End Action");
        }
    }


    protected void OnDisable(){
        _AttackTimer = 0;
        _AttackIsActive = false;
        _AttackStageTell = false;
        _AttackStageAttack = false;
        _AttackStageEnd = false;
        _TellStageStarted = false;
        _AttackStageStarted = false;
        _EndStageStarted = false;
        _AttackCoolDownActive = false;
        _CanAttackAgain= true;

    }
}
