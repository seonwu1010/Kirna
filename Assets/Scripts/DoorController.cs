using UnityEngine;
using UnityEngine.InputSystem;  // 새 Input System 사용할 때 필요

public class DoorController : MonoBehaviour
{
    [Header("Animator가 붙어 있는 DoorHinge")]
    public Animator doorAnimator;   // DoorHinge의 Animator

    [Header("Press F UI 오브젝트")]
    public GameObject interactUI;   // Canvas 밑의 Press F UI

    private bool isOpen = false;      // 문이 열려 있는지 여부
    private bool isPlayerNear = false; // 플레이어가 범위 안에 있는지 여부

    private void Start()
    {
        // Animator 자동 할당
        if (doorAnimator == null)
            doorAnimator = GetComponentInChildren<Animator>();

        // UI 시작 시 비활성화
        if (interactUI != null)
            interactUI.SetActive(false);
    }

    private void Update()
    {
        // 플레이어가 문 앞에 있을 때만 F 키 입력 처리
        if (isPlayerNear &&
            Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleDoor();
        }
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;   // 상태 반전
        doorAnimator.SetBool("isOpen", isOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            // UI 활성화
            if (interactUI != null)
                interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            // UI 비활성화
            if (interactUI != null)
                interactUI.SetActive(false);
        }
    }
}
