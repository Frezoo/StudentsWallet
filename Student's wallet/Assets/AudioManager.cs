using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private List<AudioClip> musicClips = new List<AudioClip>();

    private const string VOLUME_KEY = "MusicVolume";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolume();

            // Подписываемся на событие загрузки сцены
            SceneManager.sceneLoaded += OnSceneLoaded;

            PlaySceneMusic(); // Проигрываем музыку для начальной сцены
        }
        else
        {
            Destroy(gameObject);
        }
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
                Debug.Log($"Для сцены \"{sceneName}\" музыка не назначена.");
                break;
        }
    }

    public void PlayMusic(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < musicClips.Count)
        {
            AudioClip clip = musicClips[clipIndex];

            // Если клип уже играет — ничего не делаем
            if (musicSource.clip == clip && musicSource.isPlaying) return;

            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void PlayMusic(string clipName)
    {
        AudioClip clip = musicClips.Find(c => c.name == clipName);
        if (clip != null)
        {
            if (musicSource.clip != clip || !musicSource.isPlaying)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("Музыкальный клип с именем " + clipName + " не найден!");
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    private void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
        musicSource.volume = savedVolume;
    }
}