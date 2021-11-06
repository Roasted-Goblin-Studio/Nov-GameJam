using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerIgnore : MonoBehaviour
{
    [SerializeField] private int[] _LayersToIgnore;
    public int[] LayersToIgnore { get => _LayersToIgnore; set => _LayersToIgnore = value; }
}
