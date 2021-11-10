using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{

    FMOD.Studio.EventInstance Run;
    FMOD.Studio.EventInstance Jump;
    FMOD.Studio.EventInstance Land;
    FMOD.Studio.EventInstance Attack;

    void Start()
    {
        Run = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player/Player_Run");
        Jump = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player/Player_Jump");
        Land = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player/Player_Land");
        Attack = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Player/Player_Attack");
    }

    void Update()
    {
        
    }

    void PlayerRun()
    {
        Run.start();
    }

    void PlayerJump()
    {
        Jump.start();
    }

    void PlayerLand()
    {
        Land.start();
    }

    void PlayerAttack()
    {
        Attack.start();
    }

    void OnDestroy()
    {
        Run.release();
        Jump.release();
        Land.release();
        Attack.release();
    }
}
