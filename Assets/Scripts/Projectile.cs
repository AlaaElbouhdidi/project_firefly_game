using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    private Vector3 targetPosition;
    private Vector3 targetDir;
    private Vector3 moveDir;
    [SerializeField] private float moveSpeed = 4f;
    
    // Start is called before the first frame update
    void Start() {
        moveDir = (targetPosition - transform.position).normalized;
    }

    // Update is called once per frame
    void Update() {
        transform.position += Time.deltaTime * moveSpeed * moveDir;
    }

    public static Projectile Create(Vector3 position, Vector3 targetPosition) {
        Transform porjectileTransform = Instantiate(GameAssets.Instance.pfProjectile, position, Quaternion.identity);
        Projectile projectile = porjectileTransform.GetComponent<Projectile>();
        projectile.SetTarget(targetPosition);
        return projectile;
    }
    
    private void SetTarget(Vector3 targetPositionVector3) {
        targetPosition = targetPositionVector3;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Zone")) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && Player.Instance.GetPlayerState() == State.Blocking) {
            SoundManager.PlaySound(Sound.HeroBlock);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player") 
            && Player.Instance.GetPlayerState() != State.Blocking
            && Player.Instance.GetPlayerState() != State.Dodging) {
            PlayerHealthSystem.Instance.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
