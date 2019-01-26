using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTargetController : MonoBehaviour
{
    public bool IsFirstPlayer = false;

    public GameObject Arrow;
    public float ArrowDistantion;
    public PlayerWeaponController PlayerWeaponController;
    private SpriteRenderer _arrowSprite;
    private Vector2 _aimDirection;

    void Start()
    {
        _arrowSprite = Arrow.GetComponent<SpriteRenderer>();
    }
    public Vector2 GetThrowingDirection()
    {
        /*
        Vector3 playerPosition = new Vector2(0, 1);
        if (IsFirstPlayer)
            playerPosition = GameManager.Instance.Players[0].transform.position;
        else
            playerPosition = GameManager.Instance.Players[1].transform.position;

        Vector2 directionVector = playerPosition - Arrow.transform.localPosition ;
        var direction = directionVector / directionVector.magnitude;
        Debug.Log("<color=orange><b> ArrowTargetController.direction </b></color>"  + direction);
       
        */
        return _aimDirection;
    }

    private void Update()
    {
        _aimDirection = new Vector2(0, 0);
        if (!GlobalManager.Instance.IsScoreShowed && GlobalManager.Instance.IsCountDownEnded)
        {
            _aimDirection.x = Input.GetAxis(IsFirstPlayer ? JoystickSaver.Instanse.Joystick1.AimHorizontal : JoystickSaver.Instanse.Joystick2.AimHorizontal);
            _aimDirection.y = Input.GetAxis(IsFirstPlayer ? JoystickSaver.Instanse.Joystick1.AimVertical : JoystickSaver.Instanse.Joystick2.AimVertical);
        }
        SetArrowDirection(_aimDirection);
    }

    private void SetArrowDirection(Vector2 direction)
    {
        Arrow.transform.position = new Vector2(transform.position.x + direction.x * ArrowDistantion, transform.position.y + direction.y * ArrowDistantion);
        float movementСoefficient = Mathf.Abs(direction.x) + Mathf.Abs(direction.y);
        _arrowSprite.color = new Color32(255, 255, 255, (byte)(255 * movementСoefficient));
    }
}
