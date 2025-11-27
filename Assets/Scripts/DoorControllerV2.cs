/*using UnityEngine;
using UnityEngine.InputSystem;  // »õ Input System »ç¿ëÇÒ ¶§ ÇÊ¿ä

public class DoorController : MonoBehaviour
{
    [Header("Animator°¡ ºÙ¾î ÀÖ´Â DoorHinge")]
    public Animator doorAnimator;   // DoorHingeÀÇ Animator

    [Header("Press F UI ¿ÀºêÁ§Æ®")]
    public GameObject interactUI;   // Canvas ¹ØÀÇ Press F UI

    private bool isOpen = false;      // ¹®ÀÌ ¿­·Á ÀÖ´ÂÁö ¿©ºÎ
    private bool isPlayerNear = false; // ÇÃ·¹ÀÌ¾î°¡ ¹üÀ§ ¾È¿¡ ÀÖ´ÂÁö ¿©ºÎ

    private void Start()
    {
        // Animator ÀÚµ¿ ÇÒ´ç
        if (doorAnimator == null)
            doorAnimator = GetComponentInChildren<Animator>();

        // UI ½ÃÀÛ ½Ã ºñÈ°¼ºÈ­
        if (interactUI != null)
            interactUI.SetActive(false);
    }

    private void Update()
    {
        // ÇÃ·¹ÀÌ¾î°¡ ¹® ¾Õ¿¡ ÀÖÀ» ¶§¸¸ F Å° ÀÔ·Â Ã³¸®
        if (isPlayerNear &&
            Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleDoor();
        }
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;   // »óÅÂ ¹ÝÀü
        doorAnimator.SetBool("isOpen", isOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            // UI È°¼ºÈ­
            if (interactUI != null)
                interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            // UI ºñÈ°¼ºÈ­
            if (interactUI != null)
                interactUI.SetActive(false);
        }
    }
}

*/

using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;    // <- IMPORTANT if you use TextMeshPro

public class DoorController : MonoBehaviour
{
    [Header("Animator°¡ ºÙ¾î ÀÖ´Â DoorHinge")]
    public Animator doorAnimator;   // DoorHingeÀÇ Animator

    [Header("Press F UI ¿ÀºêÁ§Æ® (Panel)")]
    public GameObject interactUI;   // Canvas ¹ØÀÇ Press F UI Panel

    [Header("UI Text References")]
    public TextMeshProUGUI promptText;       // "Open Door (F)"
    public TextMeshProUGUI lockedMessageText; // "You have to find the key first!"

    [Header("Inventory / Key Settings")]
    public string requiredItemId = "Keycard"; // 아이템 ID (예: Keycard)

    private bool isOpen = false;        // ¹®ÀÌ ¿­·Á ÀÖ´ÂÁö ¿©ºÎ
    private bool isPlayerNear = false;  // ÇÃ·¹ÀÌ¾î°¡ ¹üÀ§ ¾È¿¡ ÀÖ´ÂÁö ¿©ºÎ

    // 현재 범위 안에 있는 플레이어 인벤토리
    private PlayerInventory currentPlayerInventory;

    [Header("Locked Message Settings")]
    public string lockedMessage = "You have to find the key first!";
    public float lockedMessageDuration = 2f; // seconds

    private void Start()
    {
        // Animator ÀÚµ¿ ÇÒ´ç
        if (doorAnimator == null)
            doorAnimator = GetComponentInChildren<Animator>();

        // UI ½ÃÀÛ ½Ã ºñÈ°¼ºÈ­
        if (interactUI != null)
            interactUI.SetActive(false);

        // Hide locked message at start
        if (lockedMessageText != null)
            lockedMessageText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // ÇÃ·¹ÀÌ¾î°¡ ¹® ¾Õ¿¡ ÀÖÀ» ¶§¸¸ F Å° ÀÔ·Â Ã³¸®
        if (isPlayerNear &&
            Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame)
        {
            TryToggleDoor();
        }
    }

    private void TryToggleDoor()
    {
        if (currentPlayerInventory == null ||
            !currentPlayerInventory.HasItem(requiredItemId))
        {
            Debug.Log("Door locked: keycard required.");
            ShowLockedMessage();
            return;
        }

        // Player has the key: make sure warning is hidden
        if (lockedMessageText != null)
            lockedMessageText.gameObject.SetActive(false);

        if (promptText != null && isPlayerNear)
            promptText.gameObject.SetActive(true);

        ToggleDoor();
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;   // »óÅÂ ¹ÝÀü
        doorAnimator.SetBool("isOpen", isOpen);
    }

    private void ShowLockedMessage()
    {
        if (lockedMessageText == null) return;

        // Hide "Open Door (F)" while we show the warning
        if (promptText != null)
            promptText.gameObject.SetActive(false);

        lockedMessageText.text = lockedMessage;
        lockedMessageText.gameObject.SetActive(true);

        // Auto-hide after some time
        StopAllCoroutines();
        StartCoroutine(HideLockedMessageAfterDelay());
    }

    private System.Collections.IEnumerator HideLockedMessageAfterDelay()
    {
        yield return new WaitForSeconds(lockedMessageDuration);

        if (lockedMessageText != null)
            lockedMessageText.gameObject.SetActive(false);

        // Bring back "Open Door (F)" if the player is still near
        if (promptText != null && isPlayerNear)
            promptText.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            // 플레이어 인벤토리 참조 저장
            currentPlayerInventory = other.GetComponent<PlayerInventory>();

            // UI È°¼ºÈ­
            if (interactUI != null)
                interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            // 범위 벗어나면 인벤토리 참조 제거
            currentPlayerInventory = null;

            // UI ºñÈ°¼ºÈ­
            if (interactUI != null)
                interactUI.SetActive(false);

            // Also hide locked message when leaving area
            if (lockedMessageText != null)
                lockedMessageText.gameObject.SetActive(false);
        }
    }
}
