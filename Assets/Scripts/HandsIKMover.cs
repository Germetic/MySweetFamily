using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsIKMover : MonoBehaviour
{

    public bool IsFirstPlayer;

    public bool IsFrontHandAttacking;
    public bool IsBackHandAttacking;

    public GameObject FHandIKObject;
    public GameObject BHandIKObject;

    public Vector3 BHandAttackRotation;
    public Vector3 FHandAttackRotation;

    public Vector3 Dir;
    public bool isSeenRight;
    public bool isHandEqualZero;

    public GameObject ArrowObject;

    public PlayerWeaponController PlayerWeaponController;

    void Update()
    {
        if (!GlobalManager.Instance.IsScoreShowed && GlobalManager.Instance.IsCountDownEnded)
        {
            IsFrontHandAttacking = Input.GetKey(IsFirstPlayer ? JoystickSaver.Instanse.Joystick1.Fire1 : JoystickSaver.Instanse.Joystick2.Fire1);
            IsBackHandAttacking = Input.GetKey(IsFirstPlayer ? JoystickSaver.Instanse.Joystick1.Fire2 : JoystickSaver.Instanse.Joystick2.Fire2);

            if (Input.GetKey(IsFirstPlayer ? JoystickSaver.Instanse.Joystick1.Fire1T : JoystickSaver.Instanse.Joystick2.Fire1T))
            {
                ThrowWeapon(true);
            }
            if (Input.GetKey(IsFirstPlayer ? JoystickSaver.Instanse.Joystick1.Fire2T : JoystickSaver.Instanse.Joystick2.Fire2T))
            {
                ThrowWeapon(false);
            }
        }
        PlayerWeaponController.SetAttackState(IsFrontHandAttacking, IsBackHandAttacking);

        Dir = new Vector3((gameObject.transform.position.x - ArrowObject.transform.position.x), (gameObject.transform.position.y - ArrowObject.transform.position.y), 0);

        var angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        Vector3 directionToRotate = Vector3.forward;
        isSeenRight = false;
        if (ArrowObject.transform.position.x > transform.position.x)
        {
            directionToRotate = Vector3.back;
            angle *= -1;
            isSeenRight = true;
        }
        isHandEqualZero = ArrowObject.transform.position.x == transform.position.x;

        if (IsFrontHandAttacking)
        {
            FHandIKObject.transform.position = ArrowObject.transform.position;
            //FHandIKObject.transform.rotation = Quaternion.Euler(FHandAttackRotation);
            //FHandIKObject.transform.LookAt(ArrowObject.transform.position);
            FHandIKObject.transform.rotation = Quaternion.AngleAxis(angle, directionToRotate);
            if (isSeenRight)
                FHandIKObject.transform.rotation *= Quaternion.Euler(0, 0, -180);
            if (isHandEqualZero)
                FHandIKObject.transform.rotation = Quaternion.Euler(Vector3.up);

        }

        if (IsBackHandAttacking)
        {

            BHandIKObject.transform.position = ArrowObject.transform.position;
            // BHandIKObject.transform.LookAt(ArrowObject.transform.position);
            BHandIKObject.transform.rotation = Quaternion.AngleAxis(angle, directionToRotate);
            if (isSeenRight)
                BHandIKObject.transform.rotation *= Quaternion.Euler(0, 0, -180);
            if (isHandEqualZero)
                BHandIKObject.transform.rotation = Quaternion.Euler(Vector3.up);
        }

    }

    private void ThrowWeapon(bool IsFrontHand)
    {
        PlayerController player = GameManager.Instance.Players[IsFirstPlayer ? 0 : 1];
        Vector2 direction = player.ArrowTargetController.GetThrowingDirection();
        player.PlayerWeaponController.ThrowWeapon(IsFrontHand,direction);
    }
}
