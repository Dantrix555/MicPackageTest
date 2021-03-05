using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class MainMenuPanel : BasePanel
{
    public void LoadPlayList()
    {
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.PlayListPanel);
    }

    public void LoadSettings()
    {
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.SettingsPanel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public override void OnPanelActive()
    {
        #if PLATFORM_ANDROID
            //Request microphone permission if is in android
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }
        #endif
    }

    public override void OnPanelStart()
    {

    }
}
