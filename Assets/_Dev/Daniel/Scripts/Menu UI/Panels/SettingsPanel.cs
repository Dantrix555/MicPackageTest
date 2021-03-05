using FMOD;
using FMODUnity;
using FMOD.Studio;
using UnityEngine;

public class SettingsPanel : BasePanel
{
    private EventInstance _musicInstance;
    private Bus _musicBus;

    public void GoBack()
    {
        _musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.MainMenuPanel);
    }

    public void SetMicrophoneScene()
    {
        _musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        MainCanvasReference.SetActiveNewPanel(MainCanvasReference.MicrophoneSelectionPanel);
    }

    public override void OnPanelActive()
    {
        //
    }

    public override void OnPanelStart()
    {
        _musicInstance = RuntimeManager.CreateInstance("event:/Music/John Lennon - Imagine (Karaoke Version)");
        _musicBus = RuntimeManager.GetBus("bus:/Master/Music");
    }

    public void SetMusicVolume(float newMusicVolume)
    {
        SoundSettingsSingleton.MusicVolume = newMusicVolume;
        _musicBus.setVolume(SoundSettingsSingleton.MusicVolume);

        PLAYBACK_STATE musicEventState;
        _musicInstance.getPlaybackState(out musicEventState);
        if (musicEventState != PLAYBACK_STATE.PLAYING)
            _musicInstance.start();
    }

    public void SetMicrophoneVolume(float microphoneVolume)
    {
        SoundSettingsSingleton.MicrophoneVolume = microphoneVolume;
    }
}
