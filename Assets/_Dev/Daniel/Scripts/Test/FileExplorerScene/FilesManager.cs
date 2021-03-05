using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Xml.Serialization;
using System.Collections.Generic;

#if UNITY_STANDALONE
using AnotherFileBrowser.Windows;
#endif

public class FilesManager : MonoBehaviour
{
    private string path;
    private AudioClip _audioClip;
    private XmlEditorSongData xmlData;
    private string xmlDataString;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private InputField _inputText;

    public void OpenMP3File()
    {
#if UNITY_STANDALONE
        var bp = new BrowserProperties();
        bp.filter = "mp3 files (*.mp3)|*.mp3";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, result =>
        {
            path = result;
        });
#endif

        StartCoroutine(LoadMP3File());
    }

    public void OpenXMLFile()
    {
#if UNITY_STANDALONE
        var bp = new BrowserProperties();
        bp.filter = "xml files (*.xml)|*.xml";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, result =>
        {
            path = result;
        });
#endif
        LoadXML(path);
        _inputText.text = xmlDataString;
    }

    public void SaveXMLFile()
    {
#if UNITY_STANDALONE
        var bp = new BrowserProperties();
        bp.filter = "txt files (*.xml)|*.xml";
        bp.filterIndex = 0;

        new FileBrowser().SaveFileBrowser(bp, "test", ".xml", result =>
        {
            path = result;

            Debug.Log(result);

            //var sr = File.CreateText(fileName);
            //sr.WriteLine("This is my file.");
            //sr.WriteLine("I can write ints {0} or floats {1}, and so on.",
            //    1, 4.2);
            //sr.Close();
        });
#endif
    }

    private IEnumerator LoadMP3File()
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                _audioClip = DownloadHandlerAudioClip.GetContent(www);
                _audioSource.clip = _audioClip;
                _audioSource.Play();
            }
        }
    }

    private void LoadXML(string filePath)
    {
        if (File.Exists(filePath))
        {
            xmlDataString = File.ReadAllText(filePath);
            XmlSerializer serializer = new XmlSerializer(typeof(XmlEditorSongData));
            using (StringReader reader = new StringReader(xmlDataString))
            {
                xmlData = serializer.Deserialize(reader) as XmlEditorSongData;
            }
        }
    }
}
