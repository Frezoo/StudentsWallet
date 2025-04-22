using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsOBJ;


    //private void Start()
    //{
    //    PlayerPrefs.DeleteAll(); // ������� ��� PlayerPrefs
    //    PlayerPrefs.Save();
    //}
    public void ChangeSettingsVisible ()
    {
        SettingsOBJ.SetActive(!SettingsOBJ.activeSelf);
    }

    public void StartGame()
    {
        // ��������� ����� NewGameSetupScene, ���� ���� ����������� �������
        // ��� GameScene ���� ���� ��� �����������.

        if (PlayerPrefs.GetInt("GameInitialized", 0) == 0)
        {
            // ��������� ����� NewGameSetupScene
            SceneManager.LoadScene("NewGameSetupScene");
        }
        else
        {
            // ��������� ����� GameScene
            SceneManager.LoadScene("GameScene");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
