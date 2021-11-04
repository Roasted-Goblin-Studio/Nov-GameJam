using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    // Pause state
    [SerializeField] private bool _GameIsPaused;
    public bool GameIsPaused { get => _GameIsPaused; set => _GameIsPaused = value; }
}
