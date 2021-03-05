using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpdateCanvas : MonoBehaviour
{
    [SerializeField] private UpdateNotification _updateNotification = default;
    public UpdateNotification UpdateNotification => _updateNotification;

    /// <summary>
    /// Initialize every needed panel
    /// </summary>
    public void InitializePanels()
    {
        _updateNotification.SetCanvasReference(this);
        _updateNotification.ForceNotificationClose();
    }
}
