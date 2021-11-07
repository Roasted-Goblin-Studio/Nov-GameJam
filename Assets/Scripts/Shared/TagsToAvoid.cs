using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagsToAvoid : MonoBehaviour
{
    [SerializeField] private string[] _TagsToAvoid;
    public string[] TagsToAvoidStrings { get => _TagsToAvoid; set => _TagsToAvoid = value; }
}
