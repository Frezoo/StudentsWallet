using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public static SettingsPanel Instance { get; private set; }

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Slider volumeSlider;

    private bool isPaused = false;

    private const string VOLUME_KEY = "MusicVolume"; // Единое имя ключа

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolume(); // Громкость загружается сразу после Awake()
    }

    void Start()
    {
        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }

        ResumeGame(); // Игра не на паузе при старте
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void QuitGame()
    {
        SaveVolume();
        Debug.Log("Выход из игры");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save();
        Debug.Log($"[SettingsPanel] Громкость установлена: {volume:F2}");
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat(VOLUME_KEY, AudioListener.volume);
        PlayerPrefs.Save();
    }

    private void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
        AudioListener.volume = savedVolume;
        Debug.Log($"[SettingsPanel] Громкость загружена: {savedVolume:F2}");
    }
}