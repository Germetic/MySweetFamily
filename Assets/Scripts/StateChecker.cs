using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class StateChecker : MonoBehaviour
{
    [Header("Common")]
    public bool IsShowGizmos;
    public PlayerController PlayerController;
    public PlayerState CurrentState;
    [Header("Grounded")]
    public GameObject IsGroundedPoint;
    public float GroundedRadius;
    public Vector3 GroundedPointOffset;
    private Vector3 IsGroundedPointStartPos;
    public Vector3 GroundedOnAirPointPos;
    public bool IsGrounded;
    public LayerMask GroundedCheckPalyers;
    [Header("Moving")]
    public bool IsMovingNow;

    private void Start()
    {
        IsGroundedPointStartPos = IsGroundedPoint.transform.localPosition;
    }
    private void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(IsGroundedPoint.transform.position + GroundedPointOffset, GroundedRadius, GroundedCheckPalyers);
        if (IsGrounded && IsMovingNow)
            CurrentState = PlayerState.Move;
        else if (IsGrounded && !IsMovingNow)
            CurrentState = PlayerState.Idle;
        else if (!IsGrounded)
            CurrentState = PlayerState.OnAir;

        CorrectIsGroundedPosition(); 
    }

    private void CorrectIsGroundedPosition()
    {
        if(CurrentState == PlayerState.OnAir)
        {
            IsGroundedPoint.transform.localPosition = GroundedOnAirPointPos;
        }
        else
        {
            IsGroundedPoint.transform.localPosition = IsGroundedPointStartPos;
        }
    }

    private void OnDrawGizmos()
    {
        if (IsShowGizmos)
        {
            Gizmos.DrawSphere(IsGroundedPoint.transform.position + GroundedPointOffset, GroundedRadius);
        }
    }
}
