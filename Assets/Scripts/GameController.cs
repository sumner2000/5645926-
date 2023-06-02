using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    //private GameObject pattern01;
    private	PatternController	patternController;

    private readonly float scoreScale = 20; // ���� ���� ��� (�б�����)

    // �÷��̾� ���� (�����ʰ� ��ƾ �ð�)
    public float CurrentScore { private set; get; } = 0;
    public bool IsGamePlay { private set; get; } = false; // ������ ���������� ��Ÿ���� bool����, false�̸� ���� ������ X

    public void GameStart() // ����ȭ���� START��ư�� ���� �� ���� ����
    {
        uiController.GameStart(); // ���ӽ��� �޼ҵ� ȣ��
        //pattern01.SetActive(true); // �� ���� Ȱ��ȭ
        patternController.GameStart();
        IsGamePlay = true; // �÷��̾��� ���� �� ���� ���� ����
    }
    public void GameExit() // ����ȭ���� EXIT��ư�� ������ ���� ����
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #else
		Application.Quit();
        #endif
    }
    public void GameOver()
    {
        uiController.GameOver();

        //pattern01.SetActive(false);
        patternController.GameOver();


        IsGamePlay = false;
    }
    private void Update()
    {
        if (IsGamePlay == false) return; // ����ȭ��, ���ȭ���� �� ������ �������� �ʵ��� return

        CurrentScore += Time.deltaTime * scoreScale;
    }

}