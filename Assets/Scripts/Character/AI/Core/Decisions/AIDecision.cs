using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AIDecision : ScriptableObject
{
    protected Decision decision;
    public abstract Decision Decide(StateController controller);
}

