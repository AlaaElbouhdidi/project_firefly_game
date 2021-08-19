using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : SingletonMonoBehaviour<GameAssets>
{
    [SerializeField] public GameObject pfEnemy;
    private new void Awake() {
        base.Awake();
    }
}
