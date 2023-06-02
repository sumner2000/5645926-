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

    private readonly float scoreScale = 20; // 점수 증가 계수 (읽기전용)

    // 플레이어 점수 (죽지않고 버틴 시간)
    public float CurrentScore { private set; get; } = 0;
    public bool IsGamePlay { private set; get; } = false; // 게임이 진행중인지 나타내는 bool변수, false이면 게임 진행중 X

    public void GameStart() // 메인화면의 START버튼을 누를 시 게임 시작
    {
        uiController.GameStart(); // 게임시작 메소드 호출
        //pattern01.SetActive(true); // 적 패턴 활성화
        patternController.GameStart();
        IsGamePlay = true; // 플레이어의 제어 및 점수 증가 가능
    }
    public void GameExit() // 메인화면의 EXIT버튼을 누를시 게임 종료
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
        if (IsGamePlay == false) return; // 메인화면, 결과화면일 때 점수가 증가하지 않도록 return

        CurrentScore += Time.deltaTime * scoreScale;
    }

}