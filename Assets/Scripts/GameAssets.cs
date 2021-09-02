using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : SingletonMonoBehaviour<GameAssets>
{
    [SerializeField] public GameObject meeleEnemy;
    [SerializeField] public GameObject rangedEnemy;
    [SerializeField] public Transform pfProjectile;
    private new void Awake() {
        base.Awake();
    }
}
