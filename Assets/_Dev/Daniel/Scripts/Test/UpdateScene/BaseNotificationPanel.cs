using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script for notifications scripts
/// </summary>
public abstract class BaseNotificationPanel : MonoBehaviour
{

    private UpdateCanvas _canvasReference;
    public UpdateCanvas CanvasReference { get => _canvasReference; set => _canvasReference = value; }

    /// <summary>
    /// Set main canvas reference to get access to its functions
    /// </summary>
    /// <param name="newCanvasReference">Canvas reference</param>
    public void SetCanvasReference(UpdateCanvas newCanvasReference)
    {
        CanvasReference = newCanvasReference;
        PopUpNotification(true);
        OnNotificationInit();
    }

    /// <summary>
    /// Pop up or disable notification
    /// </summary>
    /// <param name="popUpState">The notification pop up state</param>
    public void PopUpNotification(bool popUpState)
    {
        gameObject.SetActive(popUpState);
    }

    public void ForceNotificationClose()
    {
        PopUpNotification(false);
    }

    public abstract void OnNotificationInit();

    public abstract void OnPopUpNotification();

}
