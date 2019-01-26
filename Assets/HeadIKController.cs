using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadIKController : MonoBehaviour
{
    public Vector2 MovingDirectoryAmplitude;
    public Vector2 MovingDirectorySpeed;
    public Vector2 Offset;
    public float DamageAngleAnim;
    public float GetDamageLerpSpeed;
    private Vector2 _currentPosition;
    private Vector3 _startRotation;
    private IEnumerator _currentCoroutine;

    private float t = 0;

    private void Start()
    {
        _startRotation = transform.eulerAngles;
    }
    private void Update()
    {
        IdleStateAnim();
        if (Input.GetMouseButtonDown(0))
            GetDamageAnim();
    }

    private void IdleStateAnim()
    {
        t += Time.deltaTime;

        Vector2 sinPosition = new Vector2(Mathf.Sin(t * MovingDirectorySpeed.x) * MovingDirectoryAmplitude.x,
            Mathf.Sin(t * MovingDirectorySpeed.y) * MovingDirectoryAmplitude.y);
        Vector2 cosPosition = new Vector2(Mathf.Cos(t * MovingDirectorySpeed.x) * MovingDirectoryAmplitude.x,
    Mathf.Cos(t * MovingDirectorySpeed.y) * MovingDirectoryAmplitude.y);

        _currentPosition.x = Mathf.Cos(t * MovingDirectorySpeed.x) * MovingDirectoryAmplitude.x + transform.parent.position.x + Offset.x;
        _currentPosition.y = Mathf.Sin(t * MovingDirectorySpeed.y) * MovingDirectoryAmplitude.y + transform.parent.position.y + Offset.y;

        transform.position = _currentPosition;
    }
    private void GetDamageAnim()
    {
        //rotZ and back pinpong
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = PingPongZRotation();
        StartCoroutine(_currentCoroutine);
    }
    private IEnumerator PingPongZRotation()
    {
        float t = GetDamageLerpSpeed;

        while (transform.eulerAngles.z < DamageAngleAnim)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,DamageAngleAnim),t);
            t += 0.05f;
            yield return null;
        }
        t = GetDamageLerpSpeed;
        while (transform.eulerAngles.z > _startRotation.z)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, _startRotation.z), t);
            t += 0.05f;
            yield return null;
        }
    }
}
