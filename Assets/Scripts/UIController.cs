using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [Header("Main UI")]
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private TextMeshProUGUI textMainGrade;

    [Header("Game UI")]
    [SerializeField]
    private GameObject gamePanel;
    [SerializeField]
    private TextMeshProUGUI textScore;

    [Header("Result UI")]
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private TextMeshProUGUI textResultScore;
    [SerializeField]
    private TextMeshProUGUI textResultGrade;
    [SerializeField]
    private TextMeshProUGUI textResultTalk;
    [SerializeField]
    private TextMeshProUGUI textResultHighScore;

    [Header("Result UI Animation")]
    [SerializeField]
    private ScaleEffect effectGameOver;
    [SerializeField]
    private CountingEffect effectResultScore;
    [SerializeField]
    private FadeEffect effectResultGrade;
    private void Awake()
    {
        // ó�� ���� ���۵Ǿ� Main UI�� Ȱ��ȭ ������ �� �ְ� ��� �ҷ�����
        textMainGrade.text = PlayerPrefs.GetString("HIGHGRADE");
    }

    public void GameStart()
    {
        mainPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    public void GameOver()
    {
        int currentScore = (int)gameController.CurrentScore;

        // ���� ���� ���
        textResultScore.text = currentScore.ToString();
        // ���� ��� ���
        CalculateGradeAndTalk(currentScore);
        // �ְ� ���� ���
        CalculateHighScore(currentScore);

        gamePanel.SetActive(false);
        resultPanel.SetActive(true);

        // "���ӿ���" �ؽ�Ʈ ũ�� ��� �ִϸ��̼�
        effectGameOver.Play(500, 200);
        // ���� ������ 0���� ī�����ϴ� �ִϸ��̼�
        // ī���� �ִϸ��̼� ���� �� ��� Fade In �ִϸ��̼� ���
        effectResultScore.Play(0, currentScore, effectResultGrade.FadeIn);
    }
    public void GoToMainMenu()
    {
        // �÷��̾� ��ġ, ����, ü�� �� �ʱ�ȭ�� �� ���� ������ �׳� ������� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        textScore.text = gameController.CurrentScore.ToString("F0");
    }
    private void CalculateGradeAndTalk(int score)
    {
        if (score < 2000)
        {
            textResultGrade.text = "F";
            //textResultTalk.text = "�� ��\n����غ��ô�!";
        }
        else if (score < 3000)
        {
            textResultGrade.text = "D";
           // textResultTalk.text = "��������!";
        }
        else if (score < 4000)
        {
            textResultGrade.text = "C";
            //textResultTalk.text = "�����ϴ� �����\n���Դϴ�!";
        }
        else if (score < 5000)
        {
            textResultGrade.text = "B";
            //textResultTalk.text = "A�� ����\n�ʾҽ��ϴ�!";
        }
        else
        {
            textResultGrade.text = "A";
            //textResultTalk.text = "����Ƽ��\n�������ϴ�\n�׳�����!";
        }
    }

    private void CalculateHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");

        // �ְ� �������� ���� ������ ȹ������ ��
        if (score > highScore)
        {
            // �ְ� ��� ����
            PlayerPrefs.SetString("HIGHGRADE", textResultGrade.text);
            // �ְ� ���� ����
            PlayerPrefs.SetInt("HIGHSCORE", score);

            textResultHighScore.text = score.ToString();
        }
        else
        {
            textResultHighScore.text = highScore.ToString();
        }
    }
}
