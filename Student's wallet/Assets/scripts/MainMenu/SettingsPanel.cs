using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // Панель меню паузы
    [SerializeField] private Slider volumeSlider;    // Ползунок громкости

    private bool isPaused = false;

    private const string VOLUME_KEY = "Volume"; // Ключ для PlayerPrefs

    void Awake()
    {
        // Синглтон — сохраняем объект между сценами
        DontDestroyOnLoad(gameObject);

        // Если уже есть копия — уничтожаем текущую
        if (FindObjectsOfType<SettingsPanel>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        LoadVolume(); // Загружаем сохранённую громкость при старте

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }

        ResumeGame(); // Убедимся, что игра не на паузе при старте
    }

    void Update()
    {
        // Открытие/закрытие меню по ESC
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

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Замени на имя твоей главной сцены
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SaveVolume(); // Сохраняем громкость перед выходом
        Debug.Log("Выход из игры");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat(VOLUME_KEY, AudioListener.volume);
        PlayerPrefs.Save();
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey(VOLUME_KEY))
        {
            float savedVolume = PlayerPrefs.GetFloat(VOLUME_KEY);
            AudioListener.volume = savedVolume;
        }
        else
        {
            AudioListener.volume = 1.0f; // Громкость по умолчанию
        }
    }
}