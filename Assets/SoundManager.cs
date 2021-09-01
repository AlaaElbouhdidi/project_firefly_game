using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

    private static AudioClip unlock;
    private static AudioClip lockedDoor;
    private static AudioSource audioSource;

    private void Awake() {

        base.Awake();

        unlock = Resources.Load<AudioClip>(Sound.Unlock.ToString());
        lockedDoor = Resources.Load<AudioClip>(Sound.LockedDoor.ToString());

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f; // May be regulated by an options manu later
    }

    public static void PlaySound(Sound sound) {

        switch (sound) {

            case (Sound.Unlock):
                audioSource.PlayOneShot(unlock);
                break;
            case (Sound.LockedDoor):
                audioSource.PlayOneShot(lockedDoor);
                break;
        }
    }
}
