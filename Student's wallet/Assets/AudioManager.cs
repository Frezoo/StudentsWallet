using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private List<AudioClip> musicClips = new List<AudioClip>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic();
    }

    void PlaySceneMusic()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "MainMenu":
            case "GameScene":
                PlayMusic(0);
                break;

            case "Assistent":
            case "CallCenter":
            case "Curier":
                PlayMusic(1);
                break;

            case "lectureAudience":
                PlayMusic(2);
                break;

            default:
                Debug.Log($"[AudioManager] Для сцены \"{sceneName}\" музыка не назначена.");
                break;
        }
    }

    public void PlayMusic(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < musicClips.Count)
        {
            AudioClip clip = musicClips[clipIndex];

            if (musicSource.clip == clip && musicSource.isPlaying) return;

            musicSource.clip = musicClips[clipIndex];
            musicSource.Play();

            Debug.Log($"[AudioManager] Воспроизводим музыку: {clip.name} (Индекс: {clipIndex})");
        }
        else
        {
            Debug.LogWarning($"[AudioManager] Неверный индекс клипа: {clipIndex}");
        }
    }
}