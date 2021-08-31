using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVertical : Interactable {

    [SerializeField] private GameObject interactIcon;
    [SerializeField] private GameObject closed;


    private void Awake() {

    }

    private void Start() {

    }

    public override void Interact() {

        if (closed.activeSelf) {

            closed.SetActive(false);
            interactIcon.SetActive(false);
        }
    }

    public override void OpenInteractableIcon() {

        if (closed.activeSelf) interactIcon.SetActive(true);    // The icon should only appear when the door is closed
    }

    public override void CloseInteractableIcon() {

        interactIcon.SetActive(false);
    }
}
