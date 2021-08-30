using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {

    private BoxCollider2D useRadius;

    private GameObject interactIcon;
    private GameObject opened;
    private GameObject closed;


    private void Awake() {

    }

    private void Start() {

        useRadius = GetComponent<BoxCollider2D>();

        interactIcon = transform.Find("UIOpen").GetComponent<GameObject>();
        opened = transform.Find("opened").GetComponent<GameObject>();
        closed = transform.Find("closed").GetComponent<GameObject>();
    }

    public override void Interact() {

        if (closed.activeSelf) {

            closed.SetActive(false);
            opened.SetActive(true);
        }
    }

    public override void OpenInteractableIcon() {

        if(opened.activeSelf) interactIcon.SetActive(false);    // The icon should only appear when the door is closed
    }

    public override void CloseInteractableIcon() {

        interactIcon.SetActive(false);
    }
}
