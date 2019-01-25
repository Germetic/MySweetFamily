using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIKAnimation : MonoBehaviour
{
    public GameObject Player;
    public StateChecker StateChecker;
    public Vector3 FLegOffset;
    public Vector3 BLegOffset;
    public IKCollisionChecker FIKCollisionChecker;
    public IKCollisionChecker BIKCollisionChecker;
    public float FLegLerpSpeed;
    public float BLegLerpSpeed;
    public GameObject FLeg;
    public GameObject BLeg;
    public bool isFrontDirection;
    public float Speed;
    private float _currentSpeed;
    public float Range;
    private float _currentRange;
    private float _tempTime;
    [Space]
    public bool IsFTouch;
    public bool IsBTouch;
    public LayerMask TouchObjectsLayer;
    public float OverlapRadius;
    
    private ParamChangingChecker _isFTouchChecker;
    private ParamChangingChecker _isBTouchChecker;

    private bool IsFStaing;
    private bool IsBStaing;
    [Space]
    public Vector3 FLegOnAirPosition;
    public Vector3 BLegOnAirPosition;

    [Header("IdlePositionLegsReturning")]
    public float ReturnLegSpeed;
    public float LegsReturningFrameDelay;
    public float LegsReturningStartAnimationDelay;
    private IEnumerator _legsReturningCoroutine;
    [Space]
    public GameObject[] FLegReturnTraectoryPoints;
    private bool _isFrontLegReturningNow;
    [Space]
    public GameObject[] BLegReturnTraectoryPoints;
    private bool _isBackLegReturningNow;

    private void Start()
    {
        _isFTouchChecker = new ParamChangingChecker(true);
        _isBTouchChecker = new ParamChangingChecker(true);
    }
    void Update()
    {
        _tempTime += Time.deltaTime;

        Vector3 flegNormalPos = Player.transform.position + FLegOffset;
        Vector3 blegNormalPos = Player.transform.position + BLegOffset;

        IsFTouch = FIKCollisionChecker.IsCollidedNow;
        IsBTouch = BIKCollisionChecker.IsCollidedNow;

        if (StateChecker.CurrentState == PlayerState.OnAir)
        {
            if (_legsReturningCoroutine != null)
            {
                StopCoroutine(_legsReturningCoroutine);
                _isFrontLegReturningNow = false;
            }
                
            FLeg.transform.position = Vector3.Lerp(FLeg.transform.position, flegNormalPos + FLegOnAirPosition, FLegLerpSpeed);
            BLeg.transform.position = Vector3.Lerp(BLeg.transform.position, blegNormalPos + BLegOnAirPosition, BLegLerpSpeed);
        }
        else
        {
            float FLegNextXPoint = (Mathf.Clamp(Mathf.Cos(_tempTime * _currentSpeed), 0, 1) * _currentRange) * (isFrontDirection ? -1 : 1);
            float FLegNextYPoint = Mathf.Clamp(Mathf.Sin(_tempTime * _currentSpeed), 0, 1) * _currentRange;

            float BLegNextXPoint = (-Mathf.Clamp(Mathf.Cos(_tempTime * _currentSpeed), -1, 0) * _currentRange) * (isFrontDirection ? -1 : 1);
            float BLegNextYPoint = -Mathf.Clamp(Mathf.Sin(_tempTime * _currentSpeed), -1, 0) * _currentRange;
            if (!StateChecker.IsMovingNow)
            {

                if (!IsFTouch)
                {
                    if (!IsFStaing)
                        FLeg.transform.position = Vector3.Lerp(transform.position, (flegNormalPos + new Vector3(FLegNextXPoint, FLegNextYPoint, 0)), FLegLerpSpeed);

                }
                else
                {
                    IsFStaing = true;
                }

                if (!IsBTouch)
                {
                    if (!IsBStaing)
                        BLeg.transform.position = Vector3.Lerp(transform.position, (blegNormalPos + new Vector3(BLegNextXPoint, BLegNextYPoint, 0)), BLegLerpSpeed);
                }
                else
                {
                    IsBStaing = true;
                }
                Invoke("ReturnLegsToIdlePosition", LegsReturningStartAnimationDelay);

            }
            else
            {
                if (_legsReturningCoroutine != null)
                {
                    StopCoroutine(_legsReturningCoroutine);
                    _isFrontLegReturningNow = false;
                    _isBackLegReturningNow = false;
                }
                IsFStaing = false;
                IsBStaing = false;
                FLeg.transform.position = Vector3.Lerp(transform.position, (flegNormalPos + new Vector3(FLegNextXPoint, FLegNextYPoint, 0)), FLegLerpSpeed);
                BLeg.transform.position = Vector3.Lerp(transform.position, (blegNormalPos + new Vector3(BLegNextXPoint, BLegNextYPoint, 0)), BLegLerpSpeed);
            }

        }  

    }

    public void SwitchFacedPosition(bool isFacedRight)
    {
        FLegOffset = new Vector3((isFacedRight ? 1 : -1), FLegOffset.y, FLegOffset.z);
        BLegOffset = new Vector3((isFacedRight ? 1 : -1), BLegOffset.y, BLegOffset.z);
        isFrontDirection = isFacedRight;
    }


    private void ReturnLegsToIdlePosition()
    {
        if (!_isFrontLegReturningNow && !_isBackLegReturningNow)
        {
            GameObject[] fLegpointsArray = new GameObject[FLegReturnTraectoryPoints.Length + 1];
            fLegpointsArray[0] = FLeg;
            for (int i = 1; i < fLegpointsArray.Length; i++)
            {
                fLegpointsArray[i] = FLegReturnTraectoryPoints[i - 1];
                fLegpointsArray[i].transform.position = fLegpointsArray[i].transform.parent.position;
            }

            GameObject[] bLegpointsArray = new GameObject[BLegReturnTraectoryPoints.Length + 1];
            bLegpointsArray[0] = BLeg;
            for (int i = 1; i < bLegpointsArray.Length; i++)
            {
                bLegpointsArray[i] = BLegReturnTraectoryPoints[i - 1];
                bLegpointsArray[i].transform.position = bLegpointsArray[i].transform.parent.position;
            }

            _legsReturningCoroutine = PointsMoving(fLegpointsArray, bLegpointsArray);
            StartCoroutine(_legsReturningCoroutine);


        }
    }
    private IEnumerator PointsMoving(GameObject[] firstPointsArray,GameObject[] secondPointsArray)
    {   

        _isFrontLegReturningNow = true;
        _isBackLegReturningNow = false;


        float timeLerped = 0.0f;
        while (_isFrontLegReturningNow)
        {
            if (firstPointsArray[0].transform.position == firstPointsArray[firstPointsArray.Length - 1].transform.position)
            {
                _isFrontLegReturningNow = false;
                break;
            }

            for (int i = 0; i < firstPointsArray.Length - 1; i++)
            {
                firstPointsArray[i].transform.position = Vector3.Lerp(firstPointsArray[i].transform.position, firstPointsArray[i + 1].transform.position, timeLerped / ReturnLegSpeed);
                timeLerped += Time.deltaTime;
            }
            yield return new WaitForSeconds(LegsReturningFrameDelay);
        }

            _isBackLegReturningNow = true;
        timeLerped = 0.0f;
        while (_isBackLegReturningNow)
        {
            if (secondPointsArray[0].transform.position == secondPointsArray[secondPointsArray.Length - 1].transform.position)
            {
                _isBackLegReturningNow = false;
                break;
            }

            for (int i = 0; i < secondPointsArray.Length - 1; i++)
            {
                secondPointsArray[i].transform.position = Vector3.Lerp(secondPointsArray[i].transform.position, secondPointsArray[i + 1].transform.position, timeLerped / ReturnLegSpeed);
                timeLerped += Time.deltaTime;
            }
            yield return new WaitForSeconds(LegsReturningFrameDelay);
        }

    }
    public void SetCurrentSpeed(float speedMultipler)
    {
        _currentRange = Range * speedMultipler;
        _currentSpeed = Speed * speedMultipler;


    }
}
