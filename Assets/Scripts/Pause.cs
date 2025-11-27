using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pauseMenu;      // Panel con PAUSE, Resume, Settings, Exit
    public GameObject settingsMenu;   // Panel con MUSIC, slider y Back

    [Header("Music")]
    public Slider musicSlider;        // Slider de volumen
    public AudioSource musicSource;   // AudioSource de la música del juego

    private bool isPaused = false;

    void Start()
    {
        // Al iniciar, todo normal
        Time.timeScale = 1f;
        isPaused = false;

        if (pauseMenu != null)  pauseMenu.SetActive(false);
        if (settingsMenu != null) settingsMenu.SetActive(false);

        // Cargar volumen guardado
        if (musicSlider != null && musicSource != null)
        {
            float saved = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicSource.volume = saved;
            musicSlider.value = saved;
            musicSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }

    void Update()
    {
        // Tecla ESC para abrir/cerrar pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseMenu != null)      pauseMenu.SetActive(true);
        if (settingsMenu != null)   settingsMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseMenu != null)      pauseMenu.SetActive(false);
        if (settingsMenu != null)   settingsMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenSettings()
    {
        if (pauseMenu != null)      pauseMenu.SetActive(false);
        if (settingsMenu != null)   settingsMenu.SetActive(true);
    }

    public void BackToPause()
    {
        if (pauseMenu != null)      pauseMenu.SetActive(true);
        if (settingsMenu != null)   settingsMenu.SetActive(false);
    }

    public void ExitGame()
    {
        // En el editor: salir de PlayMode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // En build: cerrar juego
        Application.Quit();
#endif
    }

    public void ChangeVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
            PlayerPrefs.SetFloat("MusicVolume", value);
        }
    }
}