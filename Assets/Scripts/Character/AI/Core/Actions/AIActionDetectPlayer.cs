using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Core/TargetPlayer")]
public class AIActionDetectPlayer : AIAction
{
    private bool _TargetAcquired = false;
    public override void Act(StateController controller)
    {
        if(_TargetAcquired) return;
        controller.AIFlags.Target = GameObject.Find("Ronin");
    }

    private void OnDisable() {
        _TargetAcquired = false;
    }
}
