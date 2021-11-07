using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlags : MonoBehaviour
{
    // Boss has started
    [SerializeField] private bool _BossHasStarted;
    public bool BossHasStarted { get => _BossHasStarted; set => _BossHasStarted = value; }

    // Boss has started
    [SerializeField] private bool _BossIntroHasFinished;
    public bool BossIntroHasFinished { get => _BossIntroHasFinished; set => _BossIntroHasFinished = value; }
    
    // Target
    private GameObject _Target;
    public GameObject Target { get => _Target; set => _Target = value; }
    public Transform TargetLocation { get => _Target.transform; }
}
