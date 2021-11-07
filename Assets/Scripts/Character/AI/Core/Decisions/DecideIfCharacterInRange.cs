using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideIfCharacterInRange : AIDecision
{
    public override Decision Decide(StateController controller)
    {
        return decision;
    }
}
