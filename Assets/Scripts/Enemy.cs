using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour {
    private static List<Enemy> enemyList;

    [SerializeField] float maxHP = 5f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float attackCooldown = 8f;
    private Vector3 spawnPoint;
    private float currentAttackCooldown;
    private float _currentHP;
    private EnemyState state;
    private bool playerInRange = false;
    private bool inRange = false;
    private Transform player;
    private EnemyType type;
    private float patroleTime = 2f;
    private float currentPatroleTime = 0f;
    private Vector3 targetPatrolOffset;
    private bool direction = false;

    // Start is called before the first frame update
    void Start() {
        this.spawnPoint = transform.position;
        targetPatrolOffset = transform.position;
        state = EnemyState.Patrol;
        this._currentHP = maxHP;
        this.currentAttackCooldown = attackCooldown;
        this.player = Player.Instance.transform;
    }

    // Update is called once per frame
    void Update() {
        switch (state) {
            case EnemyState.Attacking:
                AttackPlayer();
                break;
            case EnemyState.Idle:
                break;
            case EnemyState.WalkingToSpawn:
                WalkToSpawn();
                break;
            case EnemyState.Patrol:
                Patrol();
                CheckAggroRange();
                break;
        }
        this.currentAttackCooldown -= Time.deltaTime;
    }

    private void Patrol() {
        if (direction && transform.position.x <= targetPatrolOffset.x) {
            targetPatrolOffset.x = spawnPoint.x + 2f;
            direction = !direction;
        } else if (!direction && transform.position.x >= targetPatrolOffset.x) {
            targetPatrolOffset.x = spawnPoint.x - 2f;
            direction = !direction;
        }
        transform.position =
            Vector3.MoveTowards(transform.position, targetPatrolOffset, moveSpeed * Time.deltaTime);
    }

    private void CheckAggroRange() {
        if (playerInRange) {
            this.state = EnemyState.Attacking;
        }
    }

    private void AttackPlayer() {
        switch (type) {
            case EnemyType.Meele:
                MeeleAttack();
                break;
            case EnemyType.Ranged:
                RangedAttack();
                break;
        }
        
    }

    private void RangedAttack() {
        if (currentAttackCooldown <= 0 && playerInRange) {
            // todo attack animation and sound
            Vector3 playerPosition = Player.Instance.transform.position;
            Projectile.Create(transform.position, playerPosition);
            currentAttackCooldown = attackCooldown;
        } else if (!playerInRange) {
            Vector3 targetDir = (player.transform.position - transform.position).normalized;
            transform.position += Time.deltaTime * moveSpeed * targetDir;
        } else {
            state = EnemyState.WalkingToSpawn;
        }
    }

    private void MeeleAttack() {
        if (Vector2.Distance(player.transform.position, transform.position) <= 1) { 
            inRange = true;
        } else { 
            inRange = false;
        }
        if (!inRange) {
            Vector3 targetDir = (player.transform.position - transform.position).normalized;
            transform.position += Time.deltaTime * moveSpeed * targetDir;
        } else if (currentAttackCooldown <= 0) {
            // todo attack animation
            State playerState = Player.Instance.GetPlayerState();
            switch (playerState) {
                case State.Blocking:
                    // todo block sound
                    break;
                case State.Dodging:
                    // dodged sound
                    break;
                default:
                    PlayerHealthSystem.Instance.TakeDamage(1);
                    currentAttackCooldown = attackCooldown;
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.tag.Equals("Zone")) {
            this.state = EnemyState.WalkingToSpawn;
        }
        else if (collider2D.CompareTag("Player")) {
            this.playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D) {
        if (collider2D.CompareTag("Player")) {
            playerInRange = false;
        }
        if (collider2D.tag.Equals("Zone")) {
            this.state = EnemyState.WalkingToSpawn;
        }
    }

    private void WalkToSpawn() {
        Vector3 targetDir = (spawnPoint - transform.position).normalized;
        transform.position += Time.deltaTime * moveSpeed * targetDir;
        if (Vector3.Distance(transform.position, spawnPoint) <= 1) {
            state = EnemyState.Patrol;
        }
    }

    private Vector3 GetPosition() {
        return transform.position;
    }

    public void TakeDamage(float damage) {
        if (state == EnemyState.WalkingToSpawn) return;
        //todo play hit sound
        this._currentHP -= damage;
        if (this._currentHP <= 0) {
            Destroy(gameObject);
            enemyList.Remove(this);
        }
    }

    public static Enemy SpawnRandom(Vector3 position) {
        GameObject enemyTransform = Instantiate(GameAssets.Instance.meeleEnemy, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        if (enemyList == null) enemyList = new List<Enemy>();
        enemy.type = EnemyType.Meele;
        enemyList.Add(enemy);
        return enemy;
    }

    public static void SpawnMeeleEnemy(Vector3 spawnPosition) {
        GameObject enemyTransform = Instantiate(GameAssets.Instance.meeleEnemy, spawnPosition, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        if (enemyList == null) enemyList = new List<Enemy>();
        enemy.type = EnemyType.Meele;
        enemyList.Add(enemy);
    }
    
    public static void SpawnRangedEnemy(Vector3 spawnPosition) {
        //todo take ranged pfEnemy
        GameObject enemyTransform = Instantiate(GameAssets.Instance.rangedEnemy, spawnPosition, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        if (enemyList == null) enemyList = new List<Enemy>();
        enemy.type = EnemyType.Ranged;
        enemyList.Add(enemy);
    }

    public static Enemy GetClosestEnemy(Vector3 position, float range) {
        if (enemyList == null) return null;
        Enemy closestEnemy = null;

        for (int i = 0; i < enemyList.Count; i++) {
            Enemy testEnemy = enemyList[i];
            if (Vector3.Distance(position, testEnemy.GetPosition()) > range) {
                continue;
            }

            if (closestEnemy == null) {
                closestEnemy = testEnemy;
            } else {
                if (Vector3.Distance(position, testEnemy.GetPosition()) <
                    Vector3.Distance(position, closestEnemy.GetPosition())) {
                    closestEnemy = testEnemy;
                }
            }
        }

        return closestEnemy;
    }
}