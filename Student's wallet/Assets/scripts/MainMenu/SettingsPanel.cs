using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // ������ ���� �����
    [SerializeField] private Slider volumeSlider;    // �������� ���������

    private bool isPaused = false;

    private const string VOLUME_KEY = "Volume"; // ���� ��� PlayerPrefs

    void Awake()
    {
        // �������� � ��������� ������ ����� �������
        DontDestroyOnLoad(gameObject);

        // ���� ��� ���� ����� � ���������� �������
        if (FindObjectsOfType<SettingsPanel>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        LoadVolume(); // ��������� ���������� ��������� ��� ������

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }

        ResumeGame(); // ��������, ��� ���� �� �� ����� ��� ������
    }

    void Update()
    {
        // ��������/�������� ���� �� ESC
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
        SceneManager.LoadScene("MainMenu"); // ������ �� ��� ����� ������� �����
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SaveVolume(); // ��������� ��������� ����� �������
        Debug.Log("����� �� ����");
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
            AudioListener.volume = 1.0f; // ��������� �� ���������
        }
    }
}