using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : SingletonMonoBehaviour<Inventory> {

    private int keyAmount = 0;

    private void Awake() {

        base.Awake();
    }

    public int GetKeyAmount() {

        return keyAmount;
    }

    public void IncreaseKeyAmount() {

        keyAmount ++;
    }

    public void DecreaseKeyAmount() {

        keyAmount--;
    }
}
