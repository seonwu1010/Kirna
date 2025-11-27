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

public class DoorController : MonoBehaviour
{
    [Header("Animator°¡ ºÙ¾î ÀÖ´Â DoorHinge")]
    public Animator doorAnimator;   // DoorHingeÀÇ Animator

    [Header("Press F UI ¿ÀºêÁ§Æ®")]
    public GameObject interactUI;   // Canvas ¹ØÀÇ Press F UI

    [Header("Inventory / Key Settings")]
    public string requiredItemId = "Keycard"; // 아이템 ID (예: Keycard)

    private bool isOpen = false;        // ¹®ÀÌ ¿­·Á ÀÖ´ÂÁö ¿©ºÎ
    private bool isPlayerNear = false;  // ÇÃ·¹ÀÌ¾î°¡ ¹üÀ§ ¾È¿¡ ÀÖ´ÂÁö ¿©ºÎ

    // 현재 범위 안에 있는 플레이어 인벤토리
    private PlayerInventory currentPlayerInventory;

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
            TryToggleDoor();
        }
    }

    private void TryToggleDoor()
    {
        // 인벤토리가 없거나, 키카드가 없으면 문 안 열림
        if (currentPlayerInventory == null ||
            !currentPlayerInventory.HasItem(requiredItemId))
        {
            Debug.Log("Door locked: keycard required.");
            return;
        }

        ToggleDoor();
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
        }
    }
}
