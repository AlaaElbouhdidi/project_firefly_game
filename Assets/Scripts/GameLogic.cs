using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : SingletonMonoBehaviour<GameLogic> {
    
    private void Start() {
        SpawnAllEnemies();
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

        InputKeyHandler.Instance.OnKeyEsc += Instance_OnKeyEsc;
    }

    private void Instance_OnKeyEsc(object sender, EventArgs e) {
        Application.Quit();
    }

    private void SpawnAllEnemies() {
        GameObject[] meeleSpawns = GameObject.FindGameObjectsWithTag("MeeleSpawn");
        foreach (GameObject spawn in meeleSpawns) {
            Enemy.SpawnMeeleEnemy(spawn.transform.position);
        }
        GameObject[] RangedSpawns = GameObject.FindGameObjectsWithTag("RangedSpawn");
        foreach (GameObject spawn in RangedSpawns) {
            Enemy.SpawnRangedEnemy(spawn.transform.position);
        }
    }
}
