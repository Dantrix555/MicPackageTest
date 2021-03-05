using UnityEngine;
using UnityEngine.UI;

public class UpdateNotification : BaseNotificationPanel
{
    [SerializeField] private Button _updateButton = default;

    public override void OnNotificationInit()
    {
        _updateButton.onClick.AddListener(GoToStore);
    }

    public override void OnPopUpNotification()
    {

    }

    /// <summary>
    /// Open the store of Android/Apple to update the app
    /// </summary>
    private void GoToStore()
    {
        //Use this to call the Android/Apple store
        Debug.Log("Opening the store to update app");

        PlayerPrefs.SetString("GameVersion", "");

        #if PLATFORM_ANDROID
        Application.OpenURL("https://play.google.com/");
        #endif
        #if PLATFORM_IOS
        Application.OpenURL("https://www.apple.com/co/app-store/");
        #endif

    }
}
