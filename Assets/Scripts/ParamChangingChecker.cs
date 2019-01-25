using System;
using UnityEngine;

public class ParamChangingChecker 
{
    private object BeforeLastFrameParameter;
    private object LastFrameParameter;

    public (bool,bool)IsChanged(object parameter)
    {
        bool isHasChangesOnCurrentFrame = false;
        bool isStayStatic = false;

        if (!LastFrameParameter.Equals(parameter))
            isHasChangesOnCurrentFrame = true;
        else
        {
            isHasChangesOnCurrentFrame = false;
            if (LastFrameParameter.Equals(BeforeLastFrameParameter))
            {
                isStayStatic = true;
            }
        }
            
        BeforeLastFrameParameter = LastFrameParameter;
        LastFrameParameter = parameter;

        return (isHasChangesOnCurrentFrame,isStayStatic);
    }
    public ParamChangingChecker(object parameter)
    {
        LastFrameParameter = parameter;
    }

   
}
