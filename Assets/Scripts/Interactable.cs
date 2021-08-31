using UnityEngine;

public abstract class Interactable : MonoBehaviour {

    private void Reset() {

        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public abstract void Interact();

    public abstract void OpenInteractableIcon();

    public abstract void CloseInteractableIcon();

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) OpenInteractableIcon();
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.CompareTag("Player")) CloseInteractableIcon();
    }
}
