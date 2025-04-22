using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [Header("Инициализация")]
    public string playerName;
    public string selectedGoal;

    [Header("Статистика")]
    public float Money;
    public float Psycho;
    public float Grade;
    public float Energy;

    [Header("Время")]
    public int Hourse;
    public int Minuts;

    [Header("Другое")]
    public GameCycle GC;



    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (PlayerPrefs.GetString("Name") != "" && PlayerPrefs.GetString("Goal") != "")
        {
            playerName = PlayerPrefs.GetString("Name");
            selectedGoal = PlayerPrefs.GetString("Goal");
        }
        Initializate();


    }

    void Initializate()
    {
        Money = PlayerPrefs.GetFloat("Money");
        Psycho = PlayerPrefs.GetFloat("Psycho");
        Grade = PlayerPrefs.GetFloat("Grade");
        Energy = PlayerPrefs.GetFloat("Energy");
        Hourse = PlayerPrefs.GetInt("Hours");
        Minuts = PlayerPrefs.GetInt("Minuts");

    }

    public void ChangeTime(int hourse,int minuts)
    {
        Hourse += hourse;
        Minuts += minuts;
        if (Minuts >= 60)
        {
            Hourse++;
            Minuts -= 60;
        }
        if (Hourse >= 24)
        {
            Hourse = Hourse % 24;
        }

    }

    private void Update()
    {
        if ((Hourse >= 3 && Hourse < 7) && (SceneManager.GetActiveScene().name == "GameScene"))
        {
            GC = GameObject.Find("UpdateUI").GetComponent<GameCycle>();
            GC.FadeScreen(8, 0.1f);
            Hourse = 7;
            Minuts = 0;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("Money", Money);
        PlayerPrefs.SetFloat("Psycho", Psycho);
        PlayerPrefs.SetFloat("Grade", Grade);
        PlayerPrefs.SetFloat("Energy", Energy);
        PlayerPrefs.SetInt("Hours", Hourse);
        PlayerPrefs.SetInt("Minuts", Minuts);
    }
}