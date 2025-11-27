using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsMenuPanel;

    [Header("UI Elements")]
    public Slider musicSlider;

    void Start()
    {
        // Asegurar que solo el main menu esté activo al inicio
        mainMenuPanel.SetActive(true);
        settingsMenuPanel.SetActive(false);

        // Volumen inicial
        if (musicSlider != null)
        {
            musicSlider.value = AudioListener.volume;
        }
    }

    // -------------------------------
    // MAIN MENU
    // -------------------------------
    public void PlayGame()
    {
        SceneManager.LoadScene("First_Floor");
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // -------------------------------
    // SETTINGS MENU
    // -------------------------------
    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsMenuPanel.SetActive(false);
    }

    public void OnMusicVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }
}
