using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameSpawner : MonoBehaviour
{
    public List<GameObject> PlayerOneModels;
    public List<GameObject> PlayerTwoModels;

    public Camera MainCamera;

    public Transform PlayerOneSpawnPoint;
    public Transform PlayerTwoSpawnPoint;

    public FirstTestLevelManager FirstTestLevelManager;
    public GameManager GameManager;
    public PlayerStatsDisplayer PlayerStatsDisplayerPl1;
    public PlayerStatsDisplayer PlayerStatsDisplayerPl2;

    [SerializeField]  private KeyCode JumpPl1 = KeyCode.Joystick1Button3;
    [SerializeField]  private KeyCode Fire1Pl1 = KeyCode.Joystick1Button5;
    [SerializeField]  private KeyCode Fire2Pl1 = KeyCode.Joystick1Button6;
    [SerializeField]  private KeyCode Fire1TPl1 = KeyCode.Joystick1Button11;
    [SerializeField]  private KeyCode Fire2TPl1 = KeyCode.Joystick1Button10;
    [SerializeField]  private string HorizontalPl1 = "Horizontal";
    [SerializeField]  private string VerticalPl1 = "Vertical";
    [SerializeField]  private string AimHorizontalPl1 = "HorizontalAim";
    [SerializeField]  private string AimVerticalPl1 = "VerticalAim";
    [SerializeField]  private KeyCode JumpPl2 = KeyCode.Joystick2Button2;
    [SerializeField]  private KeyCode Fire1Pl2 = KeyCode.Joystick2Button4;
    [SerializeField]  private KeyCode Fire2Pl2 = KeyCode.Joystick2Button5;
    [SerializeField]  private KeyCode Fire1TPl2 = KeyCode.Joystick2Button11;
    [SerializeField]  private KeyCode Fire2TPl2 = KeyCode.Joystick2Button10;
    [SerializeField]  private string HorizontalPl2 = "HorizontalJ2";
    [SerializeField]  private string VerticalPl2 = "VerticalJ2";
    [SerializeField]  private string AimHorizontalPl2 = "HorizontalAimJ2";
    [SerializeField]  private string AimVerticalPl2 = "VerticalAimJ2";


    private void Awake()
    {
        GameObject _pl1 = Instantiate(PlayerOneModels[GlobalManager.Instance.PlayerOneModel]);

        SetPlayerSettings(_pl1, true);

        GameObject _pl2 = Instantiate(PlayerTwoModels[GlobalManager.Instance.PlayerTwoModel]);

        SetPlayerSettings(_pl2, false);


    }

    private void SetPlayerSettings(GameObject _pl,bool IsFirstPlayer)
    {
        JoystickInput _ji = _pl.GetComponent<JoystickInput>();
        FinalScore.Instance.CurrentCamera = MainCamera;
        if (IsFirstPlayer)
        {
            FinalScore.Instance.PlayerOne = _pl;
            _pl.transform.position = PlayerOneSpawnPoint.position;
            FirstTestLevelManager.FirstPlayerWeaponController = _pl.GetComponent<PlayerWeaponController>();


            JoystickSaver.Instanse.Joystick1 = _ji;
            _ji.Jump = JumpPl1;
            _ji.Fire1 = Fire1Pl1;
            _ji.Fire2 = Fire2Pl1;
            _ji.Fire1T = Fire1TPl1;
            _ji.Fire2T = Fire2TPl1;
            _ji.Horizontal = HorizontalPl1;
            _ji.Vertical = VerticalPl1;
            _ji.AimHorizontal = AimHorizontalPl1;
            _ji.AimVertical = AimVerticalPl1;
            
            GameManager.Players[0] = _pl.GetComponent<PlayerController>();
            PlayerStatsDisplayerPl1.PlayerStateController = _pl.GetComponent<PlayerStateController>();
            _pl.GetComponent<PlayerStateController>().PlayerStatsDisplayer = PlayerStatsDisplayerPl1;
        } else
        {
            FinalScore.Instance.PlayerTwo = _pl;
            _pl.transform.position = PlayerTwoSpawnPoint.position;
            FirstTestLevelManager.SecondPlayerWeaponController = _pl.GetComponent<PlayerWeaponController>();
            JoystickSaver.Instanse.Joystick2 = _ji;

            _ji.Jump = JumpPl2;
            _ji.Fire1 = Fire1Pl2;
            _ji.Fire2 = Fire2Pl2;
            _ji.Fire1T = Fire1TPl2;
            _ji.Fire2T = Fire2TPl2;
            _ji.Horizontal = HorizontalPl2;
            _ji.Vertical = VerticalPl2;
            _ji.AimHorizontal = AimHorizontalPl2;
            _ji.AimVertical = AimVerticalPl2;
            
            GameManager.Players[1] = _pl.GetComponent<PlayerController>();
            PlayerStatsDisplayerPl2.PlayerStateController = _pl.GetComponent<PlayerStateController>();
            _pl.GetComponent<PlayerStateController>().PlayerStatsDisplayer = PlayerStatsDisplayerPl2;
        }
        _pl.GetComponent<PlayerController>().IsFirstPlayer = IsFirstPlayer;
        _pl.GetComponent<PlayerWeaponController>().IsFirstPlayer = IsFirstPlayer;
        _pl.GetComponentInChildren<ArrowTargetController>().IsFirstPlayer = IsFirstPlayer;
        _pl.GetComponentInChildren<HandsIKMover>().IsFirstPlayer = IsFirstPlayer;
        _pl.SetActive(true);
    }


}
