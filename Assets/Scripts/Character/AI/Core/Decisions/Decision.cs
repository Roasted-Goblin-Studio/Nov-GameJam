using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Decision {
    [SerializeField] private bool _DecisionResult = false;
    [Range(1,10)] // 1 = lowest; 10 = highest
    [SerializeField] private int _Priority = 1;

    public bool DecisionResult { get => _DecisionResult; set => _DecisionResult = value; }
    public int Priority { get => _Priority; set => _Priority = value; }
}
