using FMOD.Studio;
using FMODUnity;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayListPanel : BasePanel
{
    [Header("Songs array and buttonPrefab")]
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Transform _content;

    private string filesPath = "Assets/_Dev/Daniel/Resources/NotFMODLinkedMusic/";

    private List<Button> _buttonSongList = new List<Button>();
    private List<string> _eventNames = new List<string>();
    private List<string> _eventPaths = new List<string>();

    //Methods for streaming

    //private void LoadPathSongs()
    //{
    //    _eventNames.Clear();
    //    _eventPaths.Clear();

    //    foreach (string file in Directory.GetFiles(filesPath))
    //    {
    //        string fileName = "";

    //        if (file.Contains(".meta"))
    //            continue;
    //        else if(file.Contains(filesPath))
    //        {
    //            fileName = file.Replace(filesPath, "");
    //        }

    //        _eventNames.Add(fileName);
    //        _eventPaths.Add(file);
    //    }
    //}

    //private void LoadSongNames()
    //{
    //    //Clear button names and listener
    //    CleanItems();

    //    for (int i = 0; i < _eventNames.Count; i++)
    //    {
    //        string actualSongPath;

    //        actualSongPath = _eventPaths[i];

    //        GameObject instance = Instantiate(_buttonPrefab, _content.transform.position, Quaternion.identity, _content.transform);
    //        instance.GetComponent<Button>().GetComponentInChildren<Text>().text = _eventNames[i];
    //        instance.GetComponent<Button>().onClick.AddListener(delegate { SongButtonListener(actualSongPath); });

    //        _buttonSongList.Add(instance.GetComponent<Button>());
    //    }
    //}

    //Methods to load saved FMOD songs

    private Bank _bank;
    private EventDescription[] _eventsInBank;

    private void LoadPathSongs()
    {
        RuntimeManager.StudioSystem.getBank("bank:/Music", out _bank);
        FMOD.RESULT eventsLoadResult = _bank.getEventList(out _eventsInBank);

        _eventPaths.Clear();

        for (int i = 0; i < _eventsInBank.Length; i++)
        {
            string path;
            _eventsInBank[i].getPath(out path);

            _eventPaths.Add(path);
        }
    }

    private void LoadSongNames()
    {
        //Clear button names and listener
        CleanItems();

        for (int i = 0; i < _eventsInBank.Length; i++)
        {
            string actualSongName;

            actualSongName = _eventPaths[i].ToString();

            string songText = _eventPaths[i].Replace("event:/Music/", "");

            GameObject instance = Instantiate(_buttonPrefab, _content.transform.position, Quaternion.identity, _content.transform);
            instance.GetComponent<Button>().GetComponentInChildren<Text>().text = songText;
            instance.GetComponent<Button>().onClick.AddListener(delegate { SongButtonListener(actualSongName); });

            _buttonSongList.Add(instance.GetComponent<Button>());
        }
    }

    private void SongButtonListener(string path) => LoadKaraoke(path);

    public void LoadKaraoke(string path)
    {
        MainCanvasReference.CachedSongPath = path;
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.KaraokePanel);
    }

    private void CleanItems()
    {
        if (_buttonSongList.Count == 0) { return; }
        foreach (Button roomInfo in _buttonSongList) { Destroy(roomInfo.gameObject); }
        _buttonSongList.Clear();
    }

    public void GoBack()
    {
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.MainMenuPanel);
    }

    public override void OnPanelStart()
    {
        LoadPathSongs();
        LoadSongNames();
    }

    public override void OnPanelActive()
    {

    }
}
