using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour {

    private Collider2D roomArea;
    [SerializeField] private GameObject roomCover;

    private void Start() {

        roomArea = transform.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if(roomCover.activeSelf) roomCover.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (!roomCover.activeSelf) roomCover.SetActive(true);
    }

}
