using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    [SerializeField] private GameObject Quiz_HUD;

    [SerializeField] private Test quiz;
    [HideInInspector] public int currentTestIndex;

    [SerializeField] private float time;
    
    private float _currentTime;
    private bool _isTimerStarted;

    private PlayerLife _player;

    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text[] answersOptionsText;
    [SerializeField] private Sprite checkedBoxSprite;
    [SerializeField] private Sprite notCheckedBoxSprite;
    [SerializeField] private Button[] checkBoxes;

    [SerializeField] private Animator hurtPanelAnimator;

    [SerializeField] private GameObject goodEndPanel;
    [SerializeField] private GameObject badEndPanel;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerLife>();

        questionText.text = quiz.tests[0].question;

        answersOptionsText[0].text = quiz.tests[0].answersOptions[0];
        answersOptionsText[1].text = quiz.tests[0].answersOptions[1];
        answersOptionsText[2].text = quiz.tests[0].answersOptions[2];
        answersOptionsText[3].text = quiz.tests[0].answersOptions[3];

        _currentTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTimerStarted) _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            SkipQuestion();
        }

        foreach (var item in quiz.tests)
        {
            if (item.index == currentTestIndex)
            {
                questionText.text = item.question;

                answersOptionsText[0].text = item.answersOptions[0];
                answersOptionsText[1].text = item.answersOptions[1];
                answersOptionsText[2].text = item.answersOptions[2];
                answersOptionsText[3].text = item.answersOptions[3];
            }
        }

        timerText.text = ((int)_currentTime).ToString();
    }

    public void OnCheckBoxClick(int index)
    {
        foreach (var checkBox in checkBoxes)
        {
            if (checkBox.image.sprite == checkedBoxSprite) checkBox.image.sprite = notCheckedBoxSprite;
        }

        checkBoxes[index].image.sprite = checkBoxes[index].image.sprite == notCheckedBoxSprite ? checkedBoxSprite : notCheckedBoxSprite;
    }

    public void SkipQuestion()
    {
        int correctAnswer = 0;
        int testScore = 0;

        foreach (var test in quiz.tests)
        {
            if (test.index == currentTestIndex)
            {
                correctAnswer = test.correctAnswerIndex;
                testScore = test.score;
                break;
            }
        }

        if (_currentTime <= 0)
        {
            _player.currentLife -= testScore;
            if (currentTestIndex < quiz.tests[^1].index) hurtPanelAnimator.SetTrigger("TakeDamage");
            currentTestIndex++;
        }

        for (int i = 0; i < checkBoxes.Length; i++)
        {
            if (checkBoxes[i].image.sprite == checkedBoxSprite)
            {
                if (i != correctAnswer)
                {
                    _player.currentLife -= testScore;

                    if (currentTestIndex < quiz.tests[^1].index) hurtPanelAnimator.SetTrigger("TakeDamage");
                }

                currentTestIndex++;

                checkBoxes[i].image.sprite = notCheckedBoxSprite;

                break;
            }
        }

        if (currentTestIndex > quiz.tests[^1].index)
        {
            var lifeToWin = _player.maxLife * 60 / 100;

            if (_player.currentLife < lifeToWin)
            {
                badEndPanel.SetActive(true);
            }
            else
            {
                goodEndPanel.SetActive(true);
            }

            HideHUD();
        }

        _currentTime = time;
    }

    public void ShowHUD()
    {
        Quiz_HUD.SetActive(true);
        StartTimer();
    }

    public void HideHUD()
    {
        Quiz_HUD.SetActive(false);
    }

    public void StartTimer()
    {
        _isTimerStarted = true;
    }
}
