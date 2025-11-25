using UnityEngine;
using UnityEngine.InputSystem;  // 새 Input System 사용할 때 필요

public class DoorController : MonoBehaviour
{
    [Header("Animator가 붙어 있는 DoorHinge")]
    public Animator doorAnimator;   // DoorHinge의 Animator

    private bool isOpen = false;      // 문이 열려 있는지 여부
    private bool isPlayerNear = false; // 플레이어가 범위 안에 있는지 여부

    private void Start()
    {
        // Inspector에서 안 넣었으면 자식들 중에서 자동으로 Animator 찾기
        if (doorAnimator == null)
        {
            doorAnimator = GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        // 플레이어가 근처에 있고, 이번 프레임에 F 키가 눌렸으면
        if (isPlayerNear &&
            Keyboard.current != null &&
            Keyboard.current.fKey.wasPressedThisFrame)
        {
            ToggleDoor();
        }
    }

    private void ToggleDoor()
    {
        // 상태 토글
        isOpen = !isOpen;

        // Animator Bool 값 변경 → Transition 타고 Open/Close 애니메이션 재생
        doorAnimator.SetBool("isOpen", isOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player 태그 가진 오브젝트가 들어오면
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Player가 영역 밖으로 나가면
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
