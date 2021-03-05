using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using FMOD.Studio;
using FMOD;
using FMODUnity;

public class SoundProgramming : MonoBehaviour
{   
    EVENT_CALLBACK dialogueCallback;

    [EventRef]
    public string fmodEvent;

    void Start()
    {
        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        dialogueCallback = new EVENT_CALLBACK(DialogueEventCallback);
    }

    void PlayDialogue(string key)
    {
        EventInstance dialogueInstance = RuntimeManager.CreateInstance(fmodEvent);

        // Pin the key string in memory and pass a pointer through the user data
        GCHandle stringHandle = GCHandle.Alloc(key, GCHandleType.Pinned);
        dialogueInstance.setUserData(GCHandle.ToIntPtr(stringHandle));

        dialogueInstance.setCallback(dialogueCallback);
        dialogueInstance.start();
        dialogueInstance.release();
    }

    [AOT.MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
    static RESULT DialogueEventCallback(EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        EventInstance instance = new EventInstance(instancePtr);

        // Retrieve the user data
        IntPtr stringPtr;
        instance.getUserData(out stringPtr);

        // Get the string object
        GCHandle stringHandle = GCHandle.FromIntPtr(stringPtr);
        String key = stringHandle.Target as String;

        switch (type)
        {
            case EVENT_CALLBACK_TYPE.CREATE_PROGRAMMER_SOUND:
                {
                    MODE soundMode = MODE.LOOP_NORMAL | MODE.CREATECOMPRESSEDSAMPLE | MODE.NONBLOCKING;
                    var parameter = (PROGRAMMER_SOUND_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(PROGRAMMER_SOUND_PROPERTIES));

                    if (key.Contains("."))
                    {
                        FMOD.Sound dialogueSound;
                        var soundResult = RuntimeManager.CoreSystem.createSound(Application.streamingAssetsPath + "/" + key, soundMode, out dialogueSound);
                        if (soundResult == RESULT.OK)
                        {
                            parameter.sound = dialogueSound.handle;
                            parameter.subsoundIndex = -1;
                            Marshal.StructureToPtr(parameter, parameterPtr, false);
                        }
                    }
                    else
                    {
                        SOUND_INFO dialogueSoundInfo;
                        var keyResult = RuntimeManager.StudioSystem.getSoundInfo(key, out dialogueSoundInfo);
                        if (keyResult != RESULT.OK)
                        {
                            break;
                        }
                        Sound dialogueSound;
                        var soundResult = RuntimeManager.CoreSystem.createSound(dialogueSoundInfo.name_or_data, soundMode | dialogueSoundInfo.mode, ref dialogueSoundInfo.exinfo, out dialogueSound);
                        if (soundResult == RESULT.OK)
                        {
                            parameter.sound = dialogueSound.handle;
                            parameter.subsoundIndex = dialogueSoundInfo.subsoundindex;
                            Marshal.StructureToPtr(parameter, parameterPtr, false);
                        }
                    }
                }
                break;
            case EVENT_CALLBACK_TYPE.DESTROY_PROGRAMMER_SOUND:
                {
                    var parameter = (PROGRAMMER_SOUND_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(PROGRAMMER_SOUND_PROPERTIES));
                    var sound = new Sound();
                    sound.handle = parameter.sound;
                    sound.release();
                }
                break;
            case EVENT_CALLBACK_TYPE.DESTROYED:
                // Now the event has been destroyed, unpin the string memory so it can be garbage collected
                stringHandle.Free();
                break;
        }
        return RESULT.OK;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayDialogue("Ancient Cistern - The Legend of Zelda_ Skyward Sword");
        }
    }
}
