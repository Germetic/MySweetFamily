using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTestLevelManager : MonoBehaviour
{
    public PlayerWeaponController FirstPlayerWeaponController;

    public Weapon FirstPlayerFHandWeapon;
    public Weapon FirstPlayerBHandWeapon;
    [Space]
    public PlayerWeaponController SecondPlayerWeaponController;

    public Weapon SecondPlayerFHandWeapon;
    public Weapon SecondPlayerBHandWeapon;

    public void Start()
    {
        AddCustomWeapon();
    }
    public void AddCustomWeapon()
    {
        if(FirstPlayerWeaponController != null)
        {
            if(FirstPlayerFHandWeapon != null)
            {
                FirstPlayerWeaponController.CatchWeapon(FirstPlayerFHandWeapon,true);
            }

            if (FirstPlayerBHandWeapon != null)
            {
                FirstPlayerWeaponController.CatchWeapon(FirstPlayerBHandWeapon,false);
            }
        }
        if(SecondPlayerWeaponController != null)
        {
            if (SecondPlayerFHandWeapon != null)
            {
                SecondPlayerWeaponController.CatchWeapon(SecondPlayerFHandWeapon, true);
            }

            if (SecondPlayerBHandWeapon != null)
            {
                SecondPlayerWeaponController.CatchWeapon(SecondPlayerBHandWeapon, false);
            }
        }
    }

}
