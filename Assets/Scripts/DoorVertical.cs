using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVertical : Interactable {

    [SerializeField] private GameObject interactIcon;
    [SerializeField] private GameObject closed;
    [SerializeField] private bool needsKey; // if not, it may require a lever to open
    [SerializeField] private bool isLocked;

    private void Awake() {

    }

    private void Start() {

    }

    public override void Interact() {

        if (isLocked) {

            if (needsKey) {

                if (Inventory.Instance.GetKeyAmount() > 0) {

                    UnlockObject();
                    Inventory.Instance.DecreaseKeyAmount();
                }

                else SoundManager.PlaySound(Sound.LockedDoor2);

            }

            else Debug.Log("Must be opened from somewhere else...");

        }

        else if (closed.activeSelf) {

            OpenObject();
            closed.SetActive(false);
            interactIcon.SetActive(false);
        }
    }

    public void UnlockObject() {

        SoundManager.PlaySound(Sound.Unlock);
        isLocked = false;
        closed.SetActive(true);
    }

    public void OpenObject() {

        SoundManager.PlaySound(Sound.OpenDoor);
        closed.SetActive(false);
        interactIcon.SetActive(false);
    }

    public override void OpenInteractableIcon() {

        if (closed.activeSelf) interactIcon.SetActive(true);    // The icon should only appear when the door is closed
    }

    public override void CloseInteractableIcon() {

        interactIcon.SetActive(false);
    }
}
