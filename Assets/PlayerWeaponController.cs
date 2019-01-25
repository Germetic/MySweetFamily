using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public PlayerStateController PlayerStateController;
    public Weapon FHandWeapon;
    public Weapon BHandWeapon;
    public Transform WeaponHandleFPoint;
    public Transform WeaponHandleBPoint;

    public void SetAttackState(bool isFHandAttack, bool IsBHandAttack)
    {
        if (FHandWeapon != null)
                FHandWeapon.IsAttackingNow = isFHandAttack;
            
        if (BHandWeapon != null)
                BHandWeapon.IsAttackingNow = IsBHandAttack;

    }

    public bool IsCurrentPlayerWeapon(Weapon weapon)
    {
        bool isThisPlayerWeapon = false;
        if (FHandWeapon == weapon || BHandWeapon == weapon)
            isThisPlayerWeapon = true;

        return isThisPlayerWeapon;
    }

    /// <summary>
    /// Catching weapon to concrete hand
    /// </summary>
    /// <param name="weaponToCatch"></param>
    /// <param name="isFrontHand"></param>
    public void CatchWeapon(Weapon weaponToCatch,bool isFrontHand)
    {
        if (isFrontHand)
        {
            if(FHandWeapon != null)
            {
                FHandWeapon.Miss();
            }
            FHandWeapon = weaponToCatch;
            FHandWeapon.gameObject.transform.parent = WeaponHandleFPoint;
            FHandWeapon.Initialize(this,"Unnamed");
            weaponToCatch.Catch();
            return;
        }
        else
        {
            if (BHandWeapon != null)
            {
                BHandWeapon.Miss();
            }
            BHandWeapon = weaponToCatch;
            BHandWeapon.gameObject.transform.parent = WeaponHandleBPoint;
            BHandWeapon.Initialize(this, "Unnamed");
            weaponToCatch.Catch();
            return;
        }

    }
    /// <summary>
    /// Catching weapon to free hand
    /// </summary>
    /// <param name="weaponToCatch"></param>
    public void CatchWeapon(Weapon weaponToCatch)
    {
        if(FHandWeapon == null)
        {
            FHandWeapon = weaponToCatch;
            FHandWeapon.gameObject.transform.parent = WeaponHandleFPoint;
            FHandWeapon.Initialize(this, "Unnamed");
            weaponToCatch.Catch();
            return;
        }
        if(BHandWeapon == null)
        {
            BHandWeapon = weaponToCatch;
            BHandWeapon.gameObject.transform.parent = WeaponHandleBPoint;
            BHandWeapon.Initialize(this, "Unnamed");
            weaponToCatch.Catch();
            return;
        }
    }
    public void MissAllWeapons()
    {
        if (FHandWeapon != null)
            FHandWeapon.Miss();
        FHandWeapon = null;

        if(BHandWeapon!=null)
        BHandWeapon.Miss();
        BHandWeapon = null;
    }

}
