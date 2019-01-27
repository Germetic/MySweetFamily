using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public PlayerStateController PlayerStateController;
    public bool IsFirstPlayer = false;
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
            weaponToCatch.GetComponent<SpriteRenderer>().sortingOrder = 2;
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
            weaponToCatch.GetComponent<SpriteRenderer>().sortingOrder = -1;
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
    public void ThrowWeapon(bool isFrontHand, Vector2 diretionToThrow)
    {
        Weapon throwingWeapon = null;
        if (isFrontHand)
            throwingWeapon = (FHandWeapon != null) ? FHandWeapon : null;
        else
            throwingWeapon = (BHandWeapon != null) ? BHandWeapon : null;

        if (throwingWeapon != null)
        {
            // throwingWeapon.transform.parent = null;
            //Rigidbody2D rgb = throwingWeapon.GetComponent<Rigidbody2D>();
            //throwingWeapon.GetComponent<Collider2D>().isTrigger = false;
            // rgb.isKinematic = false;

            //rgb.AddForce(diretionToThrow * 10f,ForceMode2D.Impulse);
            throwingWeapon.ThrowTo(diretionToThrow);
        }
        else
        {
            Debug.Log("<color=orange><b> No weapon in current hand </b></color>");
        }

    }
    public void RemoveWeapon(Weapon weapon)
    {
        if (FHandWeapon == weapon)
            FHandWeapon = null;
        if (BHandWeapon == weapon)
            BHandWeapon = null;
    }

}
