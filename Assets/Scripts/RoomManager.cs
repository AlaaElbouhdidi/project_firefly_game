using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : SingletonMonoBehaviour<RoomManager> {

    [SerializeField] private Room[] rooms;

    private void Awake() {

        base.Awake();
    }

}
