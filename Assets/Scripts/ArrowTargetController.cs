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
    private Vector2 _movingDirection;

    void Start()
    {
        _arrowSprite = Arrow.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _movingDirection.x = Input.GetAxis(IsFirstPlayer ? JoystickSaver.Instanse.Joystick1.AimHorizontal : JoystickSaver.Instanse.Joystick2.AimHorizontal);
        _movingDirection.y = Input.GetAxis(IsFirstPlayer ? JoystickSaver.Instanse.Joystick1.AimVertical : JoystickSaver.Instanse.Joystick2.AimVertical);
        SetArrowDirection(_movingDirection);
    }

    private void SetArrowDirection(Vector2 direction)
    {
        Arrow.transform.position = new Vector2(transform.position.x + direction.x * ArrowDistantion, transform.position.y + direction.y * ArrowDistantion);
        float movementСoefficient = Mathf.Abs(direction.x) + Mathf.Abs(direction.y);
        _arrowSprite.color = new Color32(255, 255, 255, (byte)(255 * movementСoefficient));
    }
}
