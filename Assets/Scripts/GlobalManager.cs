using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;

    public bool IsCountDownEnded = false;
    public bool IsScoreShowed = false;

    public int PlayerOneModel = 0;
    public int PlayerTwoModel = 0;

    public int MaxScore = 20;

    public int PlayerOneScore = 0;
    public int PlayerTwoScore = 0;


    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(this);
    }
    
}

public enum ScenesNumbers
{
    Menu = 0,
    Kitchen = 1,
    Hall = 2
}
