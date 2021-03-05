using UnityEngine;
using FMOD;
using FMODUnity;

public class LoadSongStreamming : MonoBehaviour
{

    private Sound streammingMusic;
    private Channel channel;
    private ChannelGroup channelGroup;

    //Here can be referenced a song loaded from a server or a song from any path in the hard disk
    private string songPath = "Assets/_Dev/Daniel/Resources/NotFMODLinkedMusic/In My Darkest Hour - Megadeth.mp3";

    void Start()
    {
        StreamSong();
    }

    void StreamSong()
    {
        RuntimeManager.CoreSystem.createStream(songPath, MODE.DEFAULT | MODE.CREATESTREAM, out streammingMusic);
        RuntimeManager.CoreSystem.playSound(streammingMusic, channelGroup, false, out channel);

        //The out channel can be controlled after starting playing sound
        channel.setVolume(0.1f);
    }
}
