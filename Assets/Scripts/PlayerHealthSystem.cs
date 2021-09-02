using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem: SingletonMonoBehaviour<PlayerHealthSystem> {
    private float _currentHp = 6f;

    public Image heartFull1;
    public Image heartFull2;
    public Image heartFull3;

    public void TakeDamage(float damage) {
        _currentHp -= damage;
        if (_currentHp >= 4) {
            heartFull3.fillAmount -= 0.5f;
        } else if (_currentHp >= 2) {
            heartFull2.fillAmount -= 0.5f;
        } else if (_currentHp > 0) {
            heartFull1.fillAmount -= 0.5f;
        } else {
            PlayerDie();
        }
    }

    public void HealDamage(float healAmount) {
        _currentHp += healAmount;
        if (_currentHp <= 2) {
            heartFull1.fillAmount += 0.5f;
        } else if (_currentHp <= 4) {
            heartFull2.fillAmount += 0.5f;
        } else if (_currentHp <= 6) {
            heartFull3.fillAmount += 0.5f;
        }
    }

    private void PlayerDie() {
        Debug.Log("ded");
    }
}
