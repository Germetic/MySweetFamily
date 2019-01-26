using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;
    public  UnityEngine.Events.UnityEvent OnCountdownEnd;
    public UnityEngine.Events.UnityEvent OnGameOver;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        
    }
}
