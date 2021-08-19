using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private static List<Enemy> enemyList;
    
    [SerializeField] float maxHP = 5f;
    private float _currentHP;

    // Start is called before the first frame update
    void Start() {
        this._currentHP = maxHP;
    }

    // Update is called once per frame
    void Update() {
        
    }

    private Vector3 GetPosition() {
        return transform.position;
    }

    public void TakeDamage(float damage) {
        this._currentHP -= damage;
        Debug.Log(_currentHP);
        if (this._currentHP <= 0) {
            Destroy(gameObject);
            enemyList.Remove(this);
        }
    }

    public static Enemy SpawnRandom(Vector3 position) {
        GameObject enemyTransform = Instantiate(GameAssets.Instance.pfEnemy, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        if (enemyList == null) enemyList = new List<Enemy>();
        enemyList.Add(enemy);
        return enemy;
    }

    public static Enemy GetClosestEnemy(Vector3 position, float range) {
        Debug.Log(enemyList);
        if (enemyList == null) return null;
        Enemy closestEnemy = null;

        for (int i = 0; i < enemyList.Count; i++) {
            Enemy testEnemy = enemyList[i];
            if (Vector3.Distance(position, testEnemy.GetPosition()) > range) {
                continue;
            }
            if (closestEnemy == null) {
                closestEnemy = testEnemy;
            }
            else {
                if (Vector3.Distance(position, testEnemy.GetPosition()) <
                    Vector3.Distance(position, closestEnemy.GetPosition())) {
                    closestEnemy = testEnemy;
                }
            }
        }

        return closestEnemy;
    }
}
