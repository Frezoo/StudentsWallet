using UnityEngine;
using UnityEngine.SceneManagement;

public enum Job
{
    None = 0,
    Ñourier = 1,
    AccountantsAssistant = 2,
    CallÑenterOperator = 3
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [Header("Èíèöèàëèçàöèÿ")]
    public string PlayerName;
    public string SelectedGoal;
    public Job Job;


    [Header("Ñòàòèñòèêà")]
    public float Money;
    public float Psycho;
    public float Grade;
    public float Energy;
    public int FullGrade;

    [Header("Âðåìÿ")]
    public int Hourse;
    public int Minuts;
    public int Day;

    [Header("Äðóãîå")]
    public GameCycle GC;
    public bool IsPlayerWasOnLecture;
    public bool WasPayment;
    public AudioClip sleepAudio;



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
            PlayerName = PlayerPrefs.GetString("Name");
            SelectedGoal = PlayerPrefs.GetString("Goal");
        }
        Initializate();


    }

    void Initializate()
    {
        SetupInitialGameState();
        Money = PlayerPrefs.GetFloat("Money");
        Psycho = PlayerPrefs.GetFloat("Psycho");
        Grade = PlayerPrefs.GetFloat("Grade");
        Energy = PlayerPrefs.GetFloat("Energy");
        Hourse = PlayerPrefs.GetInt("Hours");
        Minuts = PlayerPrefs.GetInt("Minuts");
        Day = PlayerPrefs.GetInt("Day");
        Job = (Job)PlayerPrefs.GetInt("Job");
    }

    public void ChangeTime(int hourse, int minuts)
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
            SetNextDay();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot("screenshot.png");
            Debug.Log("Ñêðèíøîò âçÿò!");
        }

        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    Job = Job.Ñourier;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    Job = Job.CallÑenterOperator;
        //}

        if (Day % 5 == 0 && WasPayment == false)
        {
            Money += 1000;
            WasPayment = true;
        }
        else if(Day % 5 != 0)
        {
            WasPayment = false;
        }
    }

    private void SetNextDay()
    {
        GC = GameObject.Find("UpdateUI").GetComponent<GameCycle>();
        GC.FadeScreen(10, 0.1f);
        PlayActionAudio(sleepAudio);
        Hourse = 7;
        Minuts = 0;
        Day++;
        Energy += 40;
        Psycho += 8;
        if (IsPlayerWasOnLecture == false)
        {
            Grade -= 10;
            Psycho -= 10;
        }
        IsPlayerWasOnLecture = false;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("Money", Money);
        PlayerPrefs.SetFloat("Psycho", Psycho);
        PlayerPrefs.SetFloat("Grade", Grade);
        PlayerPrefs.SetFloat("Energy", Energy);
        PlayerPrefs.SetInt("Hours", Hourse);
        PlayerPrefs.SetInt("Minuts", Minuts);
        PlayerPrefs.SetInt("Day", Day);
        PlayerPrefs.SetInt("Job", (int)Job);
    }

    public void ClearSaves()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SetupInitialGameState();
    }

    public void SetupInitialGameState()
    {
        if (PlayerPrefs.GetInt("GameInitialized") == 0)
        {
            Money = 1000;
            Psycho = 100;
            Energy = 100;
            Grade = 0;
            Hourse = 7;
            Minuts = 0;
            Day = 1;
        }
    }

    public void PlayActionAudio(AudioClip clip)
    {
        GameObject.Find("ActionAudioPlayer").GetComponent<AudioSource>().PlayOneShot(clip);
    }
}