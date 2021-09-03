using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : Interactable {

    [SerializeField] private GameObject interactIcon;
    [SerializeField] private GameObject opened;
    [SerializeField] private GameObject closed;
    [SerializeField] private GameObject locked;

    public override void Interact() {

        if (locked.activeSelf) {

            if (Inventory.Instance.GetKeyAmount() > 0) {

                UnlockObject();
                Inventory.Instance.DecreaseKeyAmount();
            }

            else SoundManager.PlaySound(Sound.LockedDoor2);

        }

        else if (closed.activeSelf) OpenObject();
            

        else Debug.Log("Unexpected state of door!");
    }

    public void UnlockObject() {
        SoundManager.PlaySound(Sound.Unlock);
        locked.SetActive(false);
        closed.SetActive(true);
    }

    public void OpenObject() {

        SoundManager.PlaySound(Sound.OpenDoor);
        closed.SetActive(false);
        opened.SetActive(true);
        interactIcon.SetActive(false);
    }

    public override void OpenInteractableIcon() {

        if (closed.activeSelf || locked.activeSelf) interactIcon.SetActive(true);    // The icon should only appear when the door is closed
    }

    public override void CloseInteractableIcon() {

        interactIcon.SetActive(false);
    }
}
