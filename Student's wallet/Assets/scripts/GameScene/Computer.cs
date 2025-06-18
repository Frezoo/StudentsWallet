using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    [SerializeField] private GameObject computerUI;
    [SerializeField] private Button courierButton;
    [SerializeField] private Button callCenterButton;
    [SerializeField] private Button withoutWorkButton;

    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color defaultColor = Color.white;

    void Start()
    {
        if (computerUI != null)
        {
            computerUI.SetActive(false);
        }


        UpdateButtonsState();
    }

    private void Update()
    {
        UpdateButtonsState();
    }

    void OnMouseDown()
    {
        if (computerUI != null)
        {
            computerUI.SetActive(true);
            UpdateButtonsState(); 
        }
    }

    public void ClosePanel()
    {
        computerUI.SetActive(false);
    }

    public void SelectCourier()
    {
        if (GameManager.Instance.Grade >= 10)
        {
            GameManager.Instance.Job = Job.—ourier;
            UpdateButtonsState();
        }
    }

    public void SelectCallCenter()
    {
        if (GameManager.Instance.Grade >= 40)
        {
            GameManager.Instance.Job = Job.Call—enterOperator;
            UpdateButtonsState();
        }
    }

    public void SelectWithoutWork()
    {
        GameManager.Instance.Job = Job.None;
        UpdateButtonsState();
    }

    public void UpdateButtonsState()
    {

        courierButton.interactable = GameManager.Instance.Grade >= 10;
        callCenterButton.interactable = GameManager.Instance.Grade >= 40;
        withoutWorkButton.interactable = true; 


        ResetButtonColors();


        switch (GameManager.Instance.Job)
        {
            case Job.None:
                withoutWorkButton.image.color = selectedColor;
                break;
            case Job.—ourier:
                courierButton.image.color = selectedColor;
                break;
            case Job.Call—enterOperator:
                callCenterButton.image.color = selectedColor;
                break;
        }
    }

    private void ResetButtonColors()
    {
        courierButton.image.color = defaultColor;
        callCenterButton.image.color = defaultColor;
        withoutWorkButton.image.color = defaultColor;
    }
}