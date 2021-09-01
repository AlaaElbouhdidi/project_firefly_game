using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : Interactable {

    [SerializeField] private GameObject interactIcon;
    [SerializeField] private GameObject opened;
    [SerializeField] private GameObject closed;
    [SerializeField] private bool needsKey;

    public override void Interact() {

        if (closed.activeSelf) {

            if (needsKey) {

                if (Inventory.Instance.GetKeyAmount() > 0) {
                    OpenObject();
                    Inventory.Instance.DecreaseKeyAmount();
                }

            }

            else OpenObject();
        }
    }

    public void OpenObject() {

        closed.SetActive(false);
        opened.SetActive(true);
        interactIcon.SetActive(false);
    }

    public override void OpenInteractableIcon() {

        if (closed.activeSelf) interactIcon.SetActive(true);    // The icon should only appear when the door is closed
    }

    public override void CloseInteractableIcon() {

        interactIcon.SetActive(false);
    }
}
