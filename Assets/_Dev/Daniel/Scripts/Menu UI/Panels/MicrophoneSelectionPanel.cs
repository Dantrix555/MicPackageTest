using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD;
using FMODUnity;

public class MicrophoneSelectionPanel : BasePanel
{
    [Header("Songs array and buttonPrefab")]
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Transform _content;

    private List<Button> _microphoneList = new List<Button>();
    private int _connectedMicrophones;
    private int _connectedDrivers;

    private Guid _micGuid;
    private int _sampleRate;
    private SPEAKERMODE _fmodSpeakerMode;
    private int _channelNumber = 0;
    private DRIVER_STATE _driverState;

    private void LoadMicrophoneList()
    {
        //Clear button names and listener
        CleanItems();

        for (int i = 0; i < _connectedMicrophones; i++)
        {
            string microphoneName;
            int microphoneIndex = i;

            RuntimeManager.CoreSystem.getRecordDriverInfo(microphoneIndex, out microphoneName, 50, out _micGuid, out _sampleRate, out _fmodSpeakerMode, out _channelNumber, out _driverState);

            GameObject instance = Instantiate(_buttonPrefab, _content.transform.position, Quaternion.identity, _content.transform);
            instance.GetComponent<Button>().GetComponentInChildren<Text>().text = microphoneName;
            instance.GetComponent<Button>().onClick.AddListener(delegate { MicrophoneIndexListener(microphoneIndex); });

            _microphoneList.Add(instance.GetComponent<Button>());
        }
    }

    private void MicrophoneIndexListener(int newMicrophoneIndex) => SetMicrophoneIndex(newMicrophoneIndex);

    private void SetMicrophoneIndex(int newMicrophoneIndex)
    {
        SoundSettingsSingleton.MicrophoneIndex = newMicrophoneIndex;
        UnityEngine.Debug.Log(SoundSettingsSingleton.MicrophoneIndex);
    }

    private void CleanItems()
    {
        if (_microphoneList.Count == 0) { return; }
        foreach (Button microphoneName in _microphoneList) { Destroy(microphoneName.gameObject); }
        _microphoneList.Clear();
    }

    private void LoadMicrophonesLength()
    {
        RuntimeManager.CoreSystem.getRecordNumDrivers(out _connectedDrivers, out _connectedMicrophones);
    }

    public void GoBack()
    {
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.SettingsPanel);
    }

    public override void OnPanelActive()
    {
        //
    }

    public override void OnPanelStart()
    {
        LoadMicrophonesLength();
        LoadMicrophoneList();
    }
}
