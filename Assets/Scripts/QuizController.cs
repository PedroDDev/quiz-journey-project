using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public Test quiz;
    public int currentTestIndex;

    private PlayerLife _player;

    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text[] answersOptionsText;
    [SerializeField] private Sprite checkedBoxSprite;
    [SerializeField] private Sprite notCheckedBoxSprite;
    [SerializeField] private Button[] checkBoxes;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerLife>();

        questionText.text = quiz.tests[0].question;

        answersOptionsText[0].text = quiz.tests[0].answersOptions[0];
        answersOptionsText[1].text = quiz.tests[0].answersOptions[1];
        answersOptionsText[2].text = quiz.tests[0].answersOptions[2];
        answersOptionsText[3].text = quiz.tests[0].answersOptions[3];
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void CheckBox(int index)
    {
        if (checkBoxes[index].image.sprite == notCheckedBoxSprite) checkBoxes[index].image.sprite = checkedBoxSprite;
    }

    public void UncheckBox(int index)
    {
        if (checkBoxes[index].image.sprite == checkedBoxSprite) checkBoxes[index].image.sprite = notCheckedBoxSprite;
    }

    public void SkipQuestion()
    {
        for (int i = 0; i < checkBoxes.Length; i++)
        {
            foreach (var test in quiz.tests)
            {
                if (i == test.correctAnswerIndex)
                {
                    break;
                }
                else
                {
                    _player.currentLife -= test.score;
                    break;
                }
            }

            if (checkBoxes[i].image.sprite == checkedBoxSprite)
            {
                currentTestIndex++;
            }
            else
            {
                Debug.Log("Nenhuma Resposta selecionada");
            }
        }
    }
}
