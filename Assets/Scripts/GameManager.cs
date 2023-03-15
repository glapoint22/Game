using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler OnChange;

    // -------------------------------------------------------------------------------- Awake -------------------------------------------------------------------------------
    private void Awake()
    {
        Instance = this;
    }
    
    
    
    
    
    
    
    
    
    
    // ---------------------------------------------------------------------- Dispatch UI Change Event ----------------------------------------------------------------------
    public void DispatchUIChangeEvent()
    {
        OnChange?.Invoke(this, EventArgs.Empty);
    }
}