using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using WebAPI.Scripts;

public class CheckUpdateSingleton : BASESingleton<CheckUpdateSingleton>
{
    protected CheckUpdateSingleton() { }

    private string _gameVersion;
    public static string GameVersion { get => Instance._gameVersion; set => Instance._gameVersion = value; }

    private UpdateCanvas _canvasReference;
    public UpdateCanvas CanvasReference => _canvasReference;

    private VersionData versionData;

    private void Awake()
    {
        _canvasReference = FindObjectOfType(typeof(UpdateCanvas)) as UpdateCanvas;
        CanvasReference.InitializePanels();
        GetBackendGameVersion();
    }

    private void GetBackendGameVersion()
    {
        //WebProcedure.Instance.GetAppVersion(OnWebSuccess, ShowWebError);
    }

    /// <summary>
    /// When backend response is OK get JSON data of the actual game version
    /// </summary>
    /// <returns></returns>
    //private void OnWebSuccess(DataSnapshot data)
    //{
    //    versionData = JsonUtility.FromJson<VersionData>(data.RawJson);
    //    CheckGameVersion();
    //}

    //private void CheckGameVersion()
    //{
    //    //If is the first time the game is opened set the backend version as the actual game version
    //    if (PlayerPrefs.GetString("GameVersion") == null || PlayerPrefs.GetString("GameVersion") == "")
    //        PlayerPrefs.SetString("GameVersion", versionData.current);

    //    GameVersion = PlayerPrefs.GetString("GameVersion");

    //    if(GameVersion != versionData.current)
    //        CanvasReference.UpdateNotification.PopUpNotification(true);
    //    //Else open login interface

    //}

    //private void ShowWebError(WebError loadError)
    //{
    //    Debug.Log(loadError.Message);
    //}
}
