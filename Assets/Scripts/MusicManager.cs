using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : SingletonMonoBehaviour<MusicManager> {

    private static AudioClip forestDungeonTheme;
    private static AudioClip villageTheme;
    private static AudioClip grayVillageTheme;

    private static AudioSource audioSource;

    private void Awake() {

        base.Awake();

        forestDungeonTheme = Resources.Load<AudioClip>(Music.ForestDungeonTheme.ToString());
        villageTheme = Resources.Load<AudioClip>(Music.VillageTheme.ToString());
        grayVillageTheme = Resources.Load<AudioClip>(Music.GrayVillageTheme.ToString());

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Start() {

        // Only for testing purpose

        //PlayMusic(Music.ForestDungeonTheme);
    }

    public static void PlayMusic(Music music) {

        switch (music) {

            case (Music.ForestDungeonTheme):
                audioSource.Stop();
                audioSource.PlayOneShot(forestDungeonTheme);
                break;
            case (Music.VillageTheme):
                audioSource.Stop();
                audioSource.PlayOneShot(villageTheme);
                break;
                case (Music.GrayVillageTheme):
                audioSource.Stop();
                audioSource.PlayOneShot(grayVillageTheme);
                break;
        }
    }

}
