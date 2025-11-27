using UnityEngine;
using UnityEngine.InputSystem;

public class KeycardPickup : MonoBehaviour
{
    public string itemId = "Keycard";
    public GameObject interactUI;  // UI text like "Pickup door card"

    private bool isPlayerNear = false;
    private PlayerInventory currentInventory;

    private void Start()
    {
        if (interactUI != null)
            interactUI.SetActive(false);
    }

    private void Update()
    {
        // If player is close AND presses F
        if (isPlayerNear &&
            Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame)
        {
            PickupKeycard();
        }
    }

    private void PickupKeycard()
    {
        if (currentInventory != null)
        {
            currentInventory.AddItem(itemId);
            Debug.Log("Keycard collected!");

            if (interactUI != null)
                interactUI.SetActive(false);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            currentInventory = other.GetComponent<PlayerInventory>();

            if (interactUI != null)
                interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            currentInventory = null;

            if (interactUI != null)
                interactUI.SetActive(false);
        }
    }
}
