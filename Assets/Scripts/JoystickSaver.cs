using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickSaver : MonoBehaviour
{
    public static JoystickSaver Instanse;

    public JoystickInput Joystick1;
    public JoystickInput Joystick2;

    private void Awake()
    {
        if(Instanse == null)
        {
            Instanse = this;
        }
    }
}
