using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public Camera MainCamera;
    public float CameraMoveSpeed;
    public Vector3 MenuCameraPosition;
    public Vector3 SelectPlayerCameraPosition;

    public MenuState CurrentMenuState;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}

public enum MenuState
{
    StartGame,
    ChoosePlayer
}
