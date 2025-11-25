using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTeleport : MonoBehaviour
{
    [Header("Teleport inside same scene (optional)")]
    public Transform teleportTarget;

    [Header("Change scene (optional)")]
    public string nextSceneName = "Second_Floor";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // If a scene name is assigned ¡æ CHANGE SCENE
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        // Otherwise ¡æ TELEPORT INSIDE SAME SCENE
        if (teleportTarget != null)
        {
            StartCoroutine(TeleportPlayer(other));
        }
        else
        {
            Debug.LogWarning("SceneTeleport: No teleportTarget or nextSceneName assigned.");
        }
    }

    IEnumerator TeleportPlayer(Collider player)
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;

        player.transform.SetPositionAndRotation(
            teleportTarget.position,
            teleportTarget.rotation
        );

        yield return null;

        if (cc != null)
            cc.enabled = true;
    }
}