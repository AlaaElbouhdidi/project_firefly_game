using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : SingletonMonoBehaviour<Inventory> {

    [SerializeField] private TextMeshProUGUI keyAmountUI;

    private int keyAmount = 0;

    private void Awake() {

        base.Awake();
    }

    public int GetKeyAmount() {

        return keyAmount;
    }

    public void IncreaseKeyAmount() {

        keyAmount ++;
        UpdateKeyAmount();
    }

    public void DecreaseKeyAmount() {

        keyAmount--;
        UpdateKeyAmount();
    }

    public void UpdateKeyAmount() {
        keyAmountUI.text = keyAmount.ToString();
    }
}
