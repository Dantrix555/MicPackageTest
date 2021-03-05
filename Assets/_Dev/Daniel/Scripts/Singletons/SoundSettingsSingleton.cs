using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettingsSingleton : BASESingleton<SoundSettingsSingleton>
{
    protected SoundSettingsSingleton() { }

    private float _microphoneVolume = 4f;
    public static float MicrophoneVolume { get => Instance._microphoneVolume; set => Instance._microphoneVolume = value; }

    private float _musicVolume = 0.15f;
    public static float MusicVolume { get => Instance._musicVolume; set => Instance._musicVolume = value; }

    private int _microphoneIndex = 0;
    public static int MicrophoneIndex { get => Instance._microphoneIndex; set => Instance._microphoneIndex = value; }
}
