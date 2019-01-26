using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public bool IsFirstPlayer = false;

    public StateChecker StateChecker;
    public PlayerStateController PlayerStateController;
    public PlayerWeaponController PlayerWeaponController;
    public WalkIKAnimation WalkIKAnimation;
    public ArrowTargetController ArrowTargetController;
    public float MaxVelocity;
    public float MovingSpeed;
    private float _currentMovingSpeed;
    [SerializeField]
    private bool CanMoveHorizontal;
    [SerializeField]
    private bool CanMoveVertical;
    private Vector2 _movingDirection;

    private Rigidbody2D _rigidBody;

    ParamChangingChecker IsMovingXChanged;
    [Header("Jumping")]
    public float JumpForce;
   

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        IsMovingXChanged = new ParamChangingChecker(0f);
    }

    // Update is called once per frame
    void Update()
    {   

        Vector2 movingVelocity = _rigidBody.velocity;
        
        movingVelocity.x = _movingDirection.x * _currentMovingSpeed;
        movingVelocity.y = 0;
        _rigidBody.velocity += movingVelocity;
        _rigidBody.velocity = new Vector2(Mathf.Clamp(_rigidBody.velocity.x, -MaxVelocity, MaxVelocity), Mathf.Clamp(_rigidBody.velocity.y, -MaxVelocity, MaxVelocity));


        _movingDirection = new Vector2(CanMoveHorizontal ? Input.GetAxis(IsFirstPlayer ? JoystickSaver.Instanse.Joystick1.Horizontal : JoystickSaver.Instanse.Joystick2.Horizontal) : 0,
            CanMoveVertical ? Input.GetAxis(IsFirstPlayer ? JoystickSaver.Instanse.Joystick1.Vertical : JoystickSaver.Instanse.Joystick2.Vertical) : 0);
        //if player stay(0) and move(>0)
        float movingDirectionXAbsolute = Mathf.Abs(_movingDirection.x);
        _currentMovingSpeed =  MovingSpeed * movingDirectionXAbsolute;
        WalkIKAnimation.SetCurrentSpeed(movingDirectionXAbsolute);

        StateChecker.IsMovingNow = movingDirectionXAbsolute > 0 ? true : false;

        /*
        if (IsMovingXChanged.IsChanged(movingDirectionXAbsolute).Item1)
            Debug.Log("<color=orange><b> Changed </b></color>");
        if (IsMovingXChanged.IsChanged(movingDirectionXAbsolute).Item2)
            Debug.Log("<color=orange><b> IsStatic </b></color>");
            */
        //Moving
        //Faced LEFT
        if (_movingDirection.x < -0.1f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x) * -1, transform.lossyScale.y, transform.lossyScale.z);
            WalkIKAnimation.SwitchFacedPosition(false);
        }
        //Faced RIGHT
        else if (_movingDirection.x > 0.1f)

        {
            transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.z);
            WalkIKAnimation.SwitchFacedPosition(true);
        }
        CheckJump();
        Debug.Log("<color=orange><b> VELOCITY </b></color>" + _rigidBody.velocity);
    }

    private void CheckJump()
    {
        if (Input.GetKey( IsFirstPlayer ? JoystickSaver.Instanse.Joystick1.Jump : JoystickSaver.Instanse.Joystick2.Jump) /*&& IsGrounded*/)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (StateChecker.IsGrounded)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
            _rigidBody.AddForce(Vector2.up * JumpForce * _rigidBody.gravityScale, ForceMode2D.Impulse);
        }
    }

   
    #region MonoBehaviour
    void Reset()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    #endregion
}
