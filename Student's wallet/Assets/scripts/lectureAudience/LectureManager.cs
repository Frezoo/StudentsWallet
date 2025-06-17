using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
///  Класс для управления лекциями, отображения слайдов и проведения тестов.
/// </summary>
public class LectureManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text lectureTitleText; // Текстовое поле для отображения заголовка лекции
    [SerializeField] private TMP_Text textDisplay;      // Текстовое поле для отображения текущего слайда
    [SerializeField] private Button nextPageButton;     // Кнопка для перехода к следующему слайду/вопросу
    [SerializeField] private Button prevPageButton;     // Кнопка для перехода к предыдущему слайду
    [SerializeField] private GameObject questionPanel;  // Панель, отображающая вопросы
    [SerializeField] private TMP_Text questionTextDisplay; // Текстовое поле для отображения текста вопроса
    [SerializeField] private Button[] answerButtons;    // Массив кнопок для выбора ответов на вопрос

    [Header("Lecture Data")]
    [SerializeField] private string lecturePath;        // Путь к файлу лекции (ScriptableObject)

    private Lecture1 currentLecture;       // Текущая загруженная лекция
    private int currentPage = 0;             // Номер текущего слайда (или вопроса)
    private bool isQuizMode = false;         // Флаг, указывающий, находится ли менеджер в режиме вопросов

    private int questionNumber = 0;          // Номер текущего вопроса (в тесте)

    /// <summary>
    ///  Инициализация при старте. Загружает лекцию и отображает первый слайд.
    /// </summary>
    void Start()
    {
        // Загрузка лекции из AssetDatabase (убедитесь, что лекция находится в нужном месте)
        lecturePath = "Lecture" + PlayerPrefs.GetInt("LectureNumber");
        currentLecture = Resources.Load<Lecture1>("Lectures/" + lecturePath);
        Debug.Log("Loaded lecture: " + currentLecture);
        GameManager.Instance.FullGrade += 10;

        // Отображение заголовка лекции (если есть)
        if (lectureTitleText != null && currentLecture != null && !string.IsNullOrEmpty(currentLecture.lectureTitle))
        {
            lectureTitleText.text = currentLecture.lectureTitle;
        }

        // Отображение первого слайда
        ShowCurrentPage();
    }

    /// <summary>
    /// Обработчик нажатия на кнопку "Следующий".
    /// Переключает на следующий слайд или запускает тест.
    /// </summary>
    public void NextPage()
    {
        Debug.Log("Next");

        if (!isQuizMode)
        {
            // Режим просмотра слайдов
            if (currentPage < currentLecture.slides.Count) // Corrected condition
            {
                currentPage++;
                ShowCurrentPage(); // Отображаем следующий слайд
            }
            else
            {
                // Достигнут последний "слайд" - переходим к тесту
                StartQuiz();
            }
        }
        else
        {
            // Режим вопросов: обработка перехода к следующему вопросу будет реализована в CheckAnswer
            // (или здесь, если требуется другая логика, например, отображение результатов)
        }
    }

    /// <summary>
    /// Обработчик нажатия на кнопку "Предыдущий".
    /// Переключает на предыдущий слайд.
    /// </summary>
    public void PrevPage()
    {
        Debug.Log("Prev");
        if (currentPage > 0)
        {
            currentPage--;
            ShowCurrentPage();
        }
    }

    /// <summary>
    ///  Отображает текущий слайд (или вопрос) в UI.
    /// </summary>
    private void ShowCurrentPage()
    {
        if (currentLecture == null) return;

        if (currentPage == 0) // First page, show preface
        {
            textDisplay.text = currentLecture.preface;
        }
        else if (currentPage <= currentLecture.slides.Count) // Other slides
        {
            textDisplay.text = currentLecture.slides[currentPage - 1]; // Adjust the index if necessary.
        }
        else
        {
            //Handle out of bounds. For example, clear the text.
            textDisplay.text = "";
        }

        // Активируем/деактивируем кнопки "Следующий" и "Предыдущий" в зависимости от текущего слайда/режима
        nextPageButton.interactable = currentPage < (currentLecture.slides.Count + 1) || isQuizMode; // Corrected condition
        prevPageButton.interactable = currentPage > 0;
    }

    /// <summary>
    ///  Переключает режим на режим вопросов, отображает первый вопрос.
    /// </summary>
    private void StartQuiz()
    {
        isQuizMode = true; // Устанавливаем флаг режима вопросов
        textDisplay.gameObject.SetActive(false); // Скрываем текст слайда
        questionPanel.SetActive(true);       // Отображаем панель вопросов

        CreateQuestion(); // Отображаем первый вопрос
    }

    /// <summary>
    ///  Создает и отображает текущий вопрос и варианты ответов.
    /// </summary>
    private void CreateQuestion()
    {
        if (currentLecture.questions.Count > 0 && questionNumber < currentLecture.questions.Count)
        {
            Question currentQuestion = currentLecture.questions[questionNumber];

            questionTextDisplay.text = currentQuestion.questionText;

            // Отображаем варианты ответов на кнопки
            for (int i = 0; i < answerButtons.Length; i++) // Iterate through all answer buttons
            {
                // Ensure we have enough answer options for the buttons
                if (i < currentQuestion.answerOptions.Count)
                {
                    TMP_Text buttonText = answerButtons[i].GetComponentInChildren<TMP_Text>();
                    if (buttonText != null)
                    {
                        buttonText.text = currentQuestion.answerOptions[i];
                    }

                    Image buttonImage = answerButtons[i].GetComponent<Image>();
                    if (buttonImage == null)
                    {
                        Debug.LogError("Button " + i + " does not have an Image component!");
                        continue; // Skip to the next button
                    }

                    int answerIndex = i; // Capture the index for the lambda
                    answerButtons[i].onClick.RemoveAllListeners();
                    answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex, currentQuestion.correctAnswerIndex));
                    answerButtons[i].gameObject.SetActive(true); // Show the button
                }
                else
                {
                    // If there are fewer answer options than buttons, hide the extra buttons.
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    ///  Проверяет ответ пользователя и реагирует на правильный/неправильный ответ.
    /// </summary>
    /// <param name="selectedAnswer">Индекс выбранного ответа.</param>
    /// <param name="correctAnswer">Индекс правильного ответа.</param>
    /// <param name="pressedButton">Кнопка, на которую нажал пользователь.</param>
    private void CheckAnswer(int selectedAnswer, int correctAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            Debug.Log("Правильно!");
            answerButtons[selectedAnswer].GetComponent<Image>().color = Color.green;
            GameManager.Instance.FullGrade += 10;
            
        }
        else
        {
            // Находим вопрос, содержащий правильный ответ
            Question correctQuestion = currentLecture.questions.FirstOrDefault(q => q.correctAnswerIndex == correctAnswer);

            if (correctQuestion != null && correctAnswer >= 0 && correctAnswer < correctQuestion.answerOptions.Count)
            {
                Debug.Log("Неправильно. Правильный ответ: " + correctQuestion.answerOptions[correctAnswer]);
                answerButtons[selectedAnswer].GetComponent<Image>().color = Color.red;
            }
            else
            {
                Debug.LogWarning("Не удалось найти правильный ответ для отображения.");
            }

           
        }

        // Переход к следующему вопросу или загрузка следующей сцены
        if (questionNumber < currentLecture.questions.Count - 1)
        {
            questionNumber++;
            Debug.Log("Следующий вопрос: " + questionNumber);
            StartCoroutine(StartCreateQuestionAfterDelay(answerButtons[selectedAnswer].GetComponent<Image>())); // Задержка перед отображением следующего вопроса
        }
        else
        {
            Debug.Log("Тест завершен. Переход к следующей сцене");
            SceneManager.LoadScene(1); // Загружаем следующую сцену после завершения теста
        }
    }

    /// <summary>
    ///  Задержка перед отображением следующего вопроса.
    /// </summary>
    /// <param name="pressedButton">Кнопка, на которую нажал пользователь.</param>
    private IEnumerator StartCreateQuestionAfterDelay(Image selectedButton)
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = false; // Деактивируем кнопки
        }
        yield return new WaitForSeconds(1f); // Задержка в 1 секунду
        selectedButton.color = Color.white;
        foreach (Button button in answerButtons)
        {
            button.interactable = true; // активируем кнопки
        }
        CreateQuestion(); // Отображаем следующий вопрос
    }
}