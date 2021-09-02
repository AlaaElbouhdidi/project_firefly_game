using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable {

    [SerializeField] private Item itemLoot;

    [SerializeField] private GameObject interactIcon;
    [SerializeField] private GameObject opened;
    [SerializeField] private GameObject closed;

    private void Awake() {

    }

    private void Start() {

    }

    public override void Interact() {

        if (closed.activeSelf) {

            SoundManager.PlaySound(Sound.OpenChest);
            closed.SetActive(false);
            opened.SetActive(true);
            interactIcon.SetActive(false);
            AddToPlayersInventory();
        }
    }

    public override void OpenInteractableIcon() {

        if (closed.activeSelf) interactIcon.SetActive(true);    // The icon should only appear when the door is closed
    }

    public override void CloseInteractableIcon() {

        interactIcon.SetActive(false);
    }

    public void AddToPlayersInventory() {

        switch (itemLoot) {

            case (Item.Key):

                Inventory.Instance.IncreaseKeyAmount();
                break;
                
        }
    }
}
