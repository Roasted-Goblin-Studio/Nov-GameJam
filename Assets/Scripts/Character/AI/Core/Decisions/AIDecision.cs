using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AIDecision : ScriptableObject
{

    [SerializeField] [Range(1,10)] protected int SetPriority = 1;
    protected Decision decision;
    public abstract Decision Decide(StateController controller);

    protected void OnDisable(){
        decision = new Decision();
    }
}

