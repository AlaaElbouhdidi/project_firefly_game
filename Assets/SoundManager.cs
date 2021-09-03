using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

    private static AudioClip unlock;
    private static AudioClip lockedDoor1;
    private static AudioClip lockedDoor2;
    private static AudioClip openChest;
    private static AudioClip openDoor;
    private static AudioClip step1;
    private static AudioClip step2;

    private static AudioSource audioSource;

    private void Awake() {

        base.Awake();

        unlock = Resources.Load<AudioClip>(Sound.Unlock.ToString());
        lockedDoor1 = Resources.Load<AudioClip>(Sound.LockedDoor1.ToString());
        lockedDoor2= Resources.Load<AudioClip>(Sound.LockedDoor2.ToString());
        openChest = Resources.Load<AudioClip>(Sound.OpenChest.ToString());
        openDoor = Resources.Load<AudioClip>(Sound.OpenDoor.ToString());
        step1 = Resources.Load<AudioClip>(Sound.Step1.ToString());
        step2 = Resources.Load<AudioClip>(Sound.Step2.ToString());

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.02f; // May be regulated by an options manu later
    }

    public static void PlaySound(Sound sound) {

        switch (sound) {

            case (Sound.Unlock):
                audioSource.PlayOneShot(unlock);
                break;
            case (Sound.LockedDoor1):
                audioSource.PlayOneShot(lockedDoor1);
                break;
            case (Sound.LockedDoor2):
                audioSource.PlayOneShot(lockedDoor2);
                break;
            case (Sound.OpenChest):
                audioSource.PlayOneShot(openChest);
                break;
            case (Sound.OpenDoor):
                audioSource.PlayOneShot(openDoor);
                break;
            case (Sound.Step1):
                audioSource.PlayOneShot(step1);
                break;
            case (Sound.Step2):
                audioSource.PlayOneShot(step2);
                break;
        }
    }
}
