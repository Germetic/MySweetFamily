using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;

    public ScenesNumbers CurrentLocation = 0;

    public bool IsCountDownEnded = false;
    public bool IsScoreShowed = false;

    public int PlayerOneModel = 0;
    public int PlayerTwoModel = 0;

    public int MaxScore = 50;

    public int PlayerOneScore = 0;
    public int PlayerTwoScore = 0;


    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this);
        ClearData();
    }


    public void ClearData()
    {
        PlayerOneModel = 0;
        PlayerTwoModel = 0;
        PlayerOneScore = 0;
        PlayerTwoScore = 0;
    }
    
}

[SerializeField]
public enum ScenesNumbers
{
    Menu = 0,
    Kitchen = 1
    //Hall = 2,
    //Lenght = 3
}
