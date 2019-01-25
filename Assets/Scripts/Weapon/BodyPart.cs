using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour,ICanGetDamage
{
    public float Defence;
    public PlayerStateController PlayerStateController;

    public void GetDamage(float damage, ICanDoDamage damagedObject)
    {
        float summaryDamage = damage - Defence;
        summaryDamage = (summaryDamage < 0) ? 0 : summaryDamage;
        Defence *= 0.9f;
        Defence = (Defence < 0) ? 0 : Defence;
        //Debug.Log("<color=red><b> GET DMG </b></color>" + gameObject.name);
        PlayerStateController.GetDamage(summaryDamage, transform.position, damagedObject.GetPosition());
        DamageDisplayController.Instance.Display(transform.position, (int)summaryDamage);
        DamageDisplayController.Instance.DisplayDamager(damagedObject.GetPosition());
    }
}
