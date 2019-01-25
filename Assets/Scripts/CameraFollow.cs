using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Target;
    public Vector2 smoothSpeed;
    public float SmoothSpeedMultipler;
    public Vector3 offsetPos;

	void FixedUpdate ()
    {
        Vector3 xLerp = Vector3.Lerp(new Vector3(transform.position.x,0,offsetPos.z),new Vector3(Target.position.x,0, offsetPos.z),smoothSpeed.x);
        Vector3 yLerp = Vector3.Lerp(new Vector3(0, transform.position.y, offsetPos.z), new Vector3(0, Target.position.y, offsetPos.z), smoothSpeed.y);
       // Vector2 targetPos = Vector2.Lerp(xLerp + yLerp,Target.position,SmoothSpeedMultipler);
        transform.position = Vector3.Lerp(xLerp + yLerp , Target.position + offsetPos, SmoothSpeedMultipler);
    }
}
