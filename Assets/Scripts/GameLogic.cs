using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : SingletonMonoBehaviour<GameLogic> {
    
    private void Start() {
        SpawnAllEnemies();
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
