using FMOD;
using FMODUnity;
using FMOD.Studio;
using UnityEngine;

[RequireComponent(typeof(KaraokePanel))]
public class SongManager : MonoBehaviour
{
    private Sound _streamingSong;
    private ChannelGroup channelGroup;
    private Channel channel;

    //Streaming Methods

    //public void PlaySong(string path)
    //{
    //    StopSong();

    //    RuntimeManager.CoreSystem.createStream(path, MODE.DEFAULT | MODE.CREATESTREAM, out _streamingSong);
    //    RuntimeManager.CoreSystem.playSound(_streamingSong, channelGroup, false, out channel);

    //    //The out channel can be controlled after starting playing sound
    //    channel.setVolume(SoundSettingsSingleton.MusicVolume);
    //}

    //public void StopSong()
    //{
    //    RuntimeManager.CoreSystem.playSound(_streamingSong, channelGroup, true, out channel);
    //}

    //FMOD saved methods

    private EventInstance _songEvent;

    public void PlaySong(string path)
    {
        UnityEngine.Debug.Log(path);
        StopSong();

        _songEvent = RuntimeManager.CreateInstance(path);

        _songEvent.start();

        _songEvent.setVolume(SoundSettingsSingleton.MusicVolume);
    }

    public void StopSong()
    {
        _songEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
