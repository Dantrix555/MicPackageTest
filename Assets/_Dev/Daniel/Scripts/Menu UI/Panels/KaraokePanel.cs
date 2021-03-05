using FMOD;
using FMODUnity;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KaraokePanel : BasePanel
{
    //Public variables
    [TextArea] public string recordingDeviceName = null;
    [Header("How long in seconds before recording plays")]
    public float latency = 0f;
    [Header("Choose a key to Play/Pause/Add reverb to recording")]
    public KeyCode PlayAndPause;
    public KeyCode ReverbSwitch;

    //FMOD Objects
    private Sound _sound;
    private CREATESOUNDEXINFO _exInfo;
    private Channel _channel;
    private ChannelGroup _channelGroup;

    //How many recording devices are plugged in for us to use
    private int _driversConnected = 0;
    private int _devicesConnected = 0;

    //Info about the device we're recording with it
    private Guid _micGuid;
    private int _sampleRate;
    private SPEAKERMODE _fmodSpeakerMode;
    private int _channelNumber = 0;
    private DRIVER_STATE _driverState;

    //Control variables
    private uint _soundLength;
    private uint _samplesRecorded;
    private uint _samplesPlayed;
    private uint _driftThreshold;
    private uint _desiredLatency;
    private uint _adjustedLatency;
    private int _actualLatency;
    private uint lastRecordPos = 0;
    private uint minRecordDelta = 1;
    private uint lastPlayPos = 0;
    private bool _dspEnabled = false;
    private bool _playOrPause = true;
    private bool _playOkay = false;

    //Control Constants
    private const int _theoricalLatency = 0;
    private const int _drift = 1;

    //Extra variables
    [SerializeField] private Button _reverbButton;
    [SerializeField] private SongManager _songManager;

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(latency);
        RuntimeManager.CoreSystem.playSound(_sound, _channelGroup, true, out _channel);
        _channel.setVolume(SoundSettingsSingleton.MicrophoneVolume);
        _playOkay = true;
        UnityEngine.Debug.Log("Ready to play");
    }

    private void Update()
    {
        //adjust channel frequency
        uint recordPos;
        RuntimeManager.CoreSystem.getRecordPosition(SoundSettingsSingleton.MicrophoneIndex, out recordPos);
        
        uint recordDelta = (recordPos >= lastRecordPos) ? (recordPos - lastRecordPos) : (recordPos + _soundLength - lastRecordPos);
        lastRecordPos = recordPos;
        _samplesRecorded += recordDelta;

        if(recordDelta < minRecordDelta)
        {
            minRecordDelta = recordDelta;
            _adjustedLatency = (recordDelta <= _desiredLatency) ? _desiredLatency : recordDelta;
        }

        uint playPos;
        _channel.getPosition(out playPos, TIMEUNIT.PCM);
        uint playDelta = (playPos >= lastPlayPos) ? (playPos - lastPlayPos) : (playPos + _soundLength -lastPlayPos);
        lastPlayPos = playPos;
        _samplesPlayed = playDelta;

        int latency = (int) (_samplesRecorded - _samplesPlayed);
        _actualLatency = (int) ((0.97f * _actualLatency) + (0.03f * latency));

        int playbackRate = _sampleRate;
        if (_actualLatency < (int)(_adjustedLatency - _driftThreshold))
            playbackRate = _sampleRate - (_sampleRate / 50);
        else if (_actualLatency > (int)(_adjustedLatency + _driftThreshold))
            playbackRate = _sampleRate + (_sampleRate / 50);

        _channel.setFrequency((float)playbackRate);
    }

    public void ActiveMicrophone()
    {
        if (_playOkay)
        {
            _playOrPause = !_playOrPause;
            _channel.setPaused(_playOrPause);

            if (!_playOrPause && _dspEnabled)
                ActiveReverb();

            if (_reverbButton != null)
                _reverbButton.interactable = _playOrPause ? false : true;

        }
    }

    public void ActiveReverb()
    {
        REVERB_PROPERTIES propOn = PRESET.CONCERTHALL();
        REVERB_PROPERTIES propOff = PRESET.OFF();

        _dspEnabled = !_dspEnabled;

        RuntimeManager.CoreSystem.setReverbProperties(1, ref _dspEnabled ? ref propOn : ref propOff);
    }

    public void GoBack()
    {
        if (!_playOrPause)
            ActiveMicrophone();

        _playOkay = false;

        //Stop song from songManager
        _songManager.StopSong();

        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.PlayListPanel);
    }

    public override void OnPanelStart()
    {
        //Load song from SongManager
        _songManager.PlaySong(MainCanvasReference.CachedSongPath);

        //Step 1: Check to see if any recording devices (or drivers) are plugged in and available for us to use
        RuntimeManager.CoreSystem.getRecordNumDrivers(out _driversConnected, out _devicesConnected);
        if (_driversConnected == 0)
            UnityEngine.Debug.LogError("Please plug in a microphone");
        else
            UnityEngine.Debug.Log("You have " + _devicesConnected + " microphones connected");

        //Step 2: Get all of the information we can about the recording device (or driver) that we're going to use to record with
        RuntimeManager.CoreSystem.getRecordDriverInfo(SoundSettingsSingleton.MicrophoneIndex, out recordingDeviceName, 50, out _micGuid, out _sampleRate, out _fmodSpeakerMode, out _channelNumber, out _driverState);

        //Step 3: Store relevant information into FMOD.CREATESOUNDEXINFO variable
        _exInfo.cbsize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CREATESOUNDEXINFO));
        _exInfo.numchannels = _channelNumber;
        _exInfo.format = SOUND_FORMAT.PCM16;
        _exInfo.defaultfrequency = _sampleRate;
        _exInfo.length = (uint)_sampleRate * sizeof(short) * (uint)_channelNumber;

        //Step 4: Create an FMOD Sound "object". This is what will hold our voice as it is recorded
        RuntimeManager.CoreSystem.createSound(_exInfo.userdata, MODE.LOOP_NORMAL | MODE.OPENUSER , ref _exInfo, out _sound);

        //Step 5: Start recording through our chosen device into our Sound Object
        RuntimeManager.CoreSystem.recordStart(SoundSettingsSingleton.MicrophoneIndex, _sound, true);

        _driftThreshold = ((uint)_sampleRate * _drift) / 1000;
        _desiredLatency = ((uint)_sampleRate * _theoricalLatency) / 1000;

        _adjustedLatency = _desiredLatency;
        _actualLatency = (int)_desiredLatency;

        _sound.getLength(out _soundLength, TIMEUNIT.PCM);

        if (_reverbButton != false)
            _reverbButton.interactable = false;

        StartCoroutine(Wait());
    }

    public override void OnPanelActive()
    {

    }
}
