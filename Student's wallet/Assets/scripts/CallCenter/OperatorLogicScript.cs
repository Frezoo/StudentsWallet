using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OperatorLogicScript : MonoBehaviour
{
    [Header("Текст")]
    public TextMeshProUGUI Question;
    public TextMeshProUGUI Answer1;
    public TextMeshProUGUI Answer2;
    public TextMeshProUGUI Answer3;

    [Header("Кнопки")]
    public List<GameObject> Buttons;

    [Header("Изображения")]
    public Image TimerIMG;

    [Header("Вопросы И Ответы")]

    public string[] questions = {
 "С моего счета списали 5000 руб., но я ничего не покупал!",
 "Почему у меня заблокировалась карта без предупреждения?",
 "Мне пришел счет за услугу, которой я не пользовался!",
 "Почему вчера интернет работал медленнее обычного?",
 "Меня перевели не на того оператора уже третий раз!",
 "Как мне подключить роуминг на поездку за границу?",
 "Хочу сменить тариф, но не вижу новых вариантов",
 "Почему у меня закончился интернет раньше срока?",
 "Как отключить автопродление подписки?",
 "Можно ли увеличить скорость интернета без смены тарифа?",
 "У меня не работает мобильный интернет",
 "Почему я не могу войти в личный кабинет?",
 "Звонки прерываются через 10 секунд",
 "SMS не отправляются уже час",
 "Приложение постоянно вылетает",
 "Я жду ответа уже 30 минут! Это безобразие!",
 "Меня уже пятый оператор переключает!",
 "Вы мне нахамили в прошлый раз!",
 "Я требую компенсацию за ваши ошибки!",
 "Я подаю в суд за ваш обман!",
 "Я потерял телефон, как восстановить SIM-карту?",
 "Мне звонят мошенники от вашего имени!",
 "Мой ребёнок купил подписку на 10 000 руб.!",
 "Я переехал в другой город, что делать с тарифом?",
 "Вы мне обещали скидку, но её нет!"
};

    public string[][] answers = {
 new string[] { "Сейчас проверю транзакции и помогу вам разобраться", "Это ваши проблемы", "Наверное, вы сами что-то купили" },
 new string[] { "Проверю причину блокировки и помогу восстановить доступ", "Карта не наша, звоните в банк", "Это технический сбой, подождите неделю" },
 new string[] { "Извините за недоразумение, сейчас разберусь и отменю начисление", "Оплатите, потом разберёмся", "Это автоматическое списание, ничего не поделаешь" },
 new string[] { "Проверю возможные сбои и предложу компенсацию", "Это не наша проблема", "Наверное, у вас старый роутер" },
 new string[] { "Сейчас решу ваш вопрос лично, чтобы не переключать", "Система автоматическая, ничего не поделаешь", "Перезвоните позже" },
 new string[] { "Сейчас расскажу, это можно сделать через приложение или SMS-команду", "Это дорого, лучше не подключайте", "Звоните в поддержку за границей" },
 new string[] { "Помогу подобрать подходящий тариф и расскажу, как его активировать", "Новых тарифов нет, выбирайте из старых", "Это можно сделать только в офисе" },
 new string[] { "Проверю статистику и предложу дополнительные пакеты", "Вы слишком много тратите", "Это ваша вина" },
 new string[] { "Сейчас объясню, как это сделать в личном кабинете", "Это невозможно, пока не закончится срок", "Пишите на почту поддержки" },
 new string[] { "Да, есть платные опции, расскажу подробнее", "Нет, только если перейдёте на дорогой тариф", "Скорость фиксированная, ничего не сделаешь" },
 new string[] { "Проверю настройки и отправлю вам инструкцию для восстановления", "Перезагрузите телефон и не беспокойте нас", "Это проблема вашего телефона" },
 new string[] { "Помогу восстановить доступ: проверьте почту для кода", "Сайт временно не работает", "Ваш аккаунт, наверное, удалён" },
 new string[] { "Проверю качество связи в вашем районе", "Купите новый телефон", "Это не наша зона покрытия" },
 new string[] { "Проверю возможные сбои в сервисе", "Подождите, может, само заработает", "Ваш телефон не поддерживает SMS" },
 new string[] { "Посоветую обновить его или переустановить", "Купите новый телефон", "Это не наша проблема" },
 new string[] { "Очень извиняюсь за ожидание, как я могу вам помочь?", "Очередь большая, что вы хотели?", "Если кричать, я не буду помогать" },
 new string[] { "Сейчас решу ваш вопрос лично, чтобы не переводить", "Система автоматическая, терпите", "Перезвоните в другое время" },
 new string[] { "Приношу извинения, давайте решим ваш вопрос сейчас", "Это был не я", "Если вам не нравится — уходите" },
 new string[] { "Рассмотрим вашу претензию и предложим решение", "Компенсаций не предусмотрено", "Вы сами виноваты" },
 new string[] { "Давайте разберём ситуацию, чтобы избежать недоразумений", "Делайте что хотите", "Наш юрист вас разорит" },
 new string[] { "Помогу оформить дубликат, нужен паспорт", "Без телефона нельзя", "Приходите в офис (даже если клиент за границей)" },
 new string[] { "Предупредим о новых схемах обмана", "Мы тут ни при чём", "Просто не отвечайте" },
 new string[] { "Помогу оформить возврат, нужны данные карты", "Родители должны следить за детьми", "Деньги не вернём" },
 new string[] { "Подберу оптимальный вариант для нового региона", "Платите как раньше", "Теряйте номер" },
 new string[] { "Проверю запись разговора и исправлю ситуацию", "Нам ничего не известно", "Скидки закончились" }
};

    public int[] correctAnswers = {
 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
};

    [Header("Конфигурация")]

    public int CurrentQuestionIndex = 0;

    public int MoneyAward = 0;

    public int CountCorrectAnswerInTime;
    public int CountCorrectAnswerOutTime;
    public int CountUncorrectAnswer;

    public float TimeToAnswer = 10f;
    public float InitialTimeToAnswer = 10f;

    public Color EndColor;
    public Color StartColor;

    [Header("Прочее")]
    public EndGamePanel EGP;


    private void Start()
    {
        EGP = GetComponent<EndGamePanel>();
        LoadQuestion(0);
    }

    private void Update()
    {
        TimeToAnswer -= Time.deltaTime;
        UpdateTimerImage();
    }

    void UpdateTimerImage()
    {
        float fillAmount = TimeToAnswer / InitialTimeToAnswer;
        TimerIMG.fillAmount = fillAmount;
        TimerIMG.color = Color.Lerp(EndColor, StartColor, fillAmount);
    }

    void LoadQuestion(int questionIndex)
    {
        if (questionIndex >= questions.Length)
        {
            EGP.GetConfigValues(CountCorrectAnswerInTime, CountCorrectAnswerOutTime, CountUncorrectAnswer, MoneyAward);
            EGP.TurnEndPanel();
            return;
        }
        ShuffleButtonPositions();
        Question.text = questions[questionIndex];
        Answer1.text = answers[questionIndex][0];
        Answer2.text = answers[questionIndex][1];
        Answer3.text = answers[questionIndex][2];
    }

    public void AnswerButtonClicked(int answerIndex)
    {
        if (answerIndex == correctAnswers[CurrentQuestionIndex])
        {
            if (TimeToAnswer >= 0)
            {
                Debug.Log("Правильный ответ!");
                MoneyAward += 400;
                CountCorrectAnswerInTime++;
            }
            else
            {
                Debug.Log("Правильный ответ,но время не соблюдено!");
                MoneyAward += 200;
                CountCorrectAnswerOutTime++;
            }
            
            
        }
        else
        {
            Debug.Log("Неправильный ответ!");
            CountUncorrectAnswer++;
        }

        CurrentQuestionIndex++;
        TimeToAnswer = InitialTimeToAnswer;
        LoadQuestion(CurrentQuestionIndex);
    }

    public void ShuffleButtonPositions()
    {

        List<Vector3> positions = Buttons.Select(b => b.transform.position).ToList();

        positions = positions.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < Buttons.Count; i++)
        {
            Buttons[i].transform.position = positions[i];
        }
    }

}
