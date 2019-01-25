using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKCollisionChecker : MonoBehaviour
{
    public bool IsCollidedNow{ get { return IsCollidingNow(); } private set { } }
    public LayerMask LayersToCheck;
    private Vector2 _firstColliderPoint;
    private Vector2 _firstColliderPointNormal;
    public float CheckerRoundRadius;
    public bool isInversed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((LayersToCheck.value & (1 << collision.gameObject.layer)) != 0)
        {
            _firstColliderPointNormal = collision.contacts[0].normal;
            _firstColliderPoint = collision.contacts[0].point;
        }
    }

    private bool IsCollidingNow()
    {
        return Physics2D.OverlapCircle(transform.position, CheckerRoundRadius, LayersToCheck);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position,CheckerRoundRadius); 
    }
    public float GetGroundAngle()
    {
        Vector2 pointA = _firstColliderPoint;
        Vector2 pointB = pointA + _firstColliderPointNormal;
        Vector2 pointC = new Vector2(pointB.x, pointA.y);

        float BCSideLen = Vector2.Distance(pointB, pointC);
        float BASideLen = Vector2.Distance(pointB, pointA);

        float angle = Mathf.Asin(BCSideLen / BASideLen) * Mathf.Rad2Deg;
        isInversed = pointC.x < pointA.x;

        return isInversed?-angle:angle;
    }
}
