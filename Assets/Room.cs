using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour {

    private BoxCollider2D roomArea;
    [SerializeField] private GameObject roomCover;

    private void Start() {

        roomArea = transform.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if(roomCover.activeSelf) roomCover.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!roomCover.activeSelf) roomCover.SetActive(true);
    }


}
