using UnityEngine;
using UnityEngine.Android;

public class MicrophoneTest : MonoBehaviour
{

    private AudioSource _audioSource;

    void Start()
    {
#if PLATFORM_ANDROID
        //Request microphone permission if is in android
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif

        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = Microphone.Start(Microphone.devices[0], true, 10, 48000);
        _audioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        _audioSource.Play();
    }
}
