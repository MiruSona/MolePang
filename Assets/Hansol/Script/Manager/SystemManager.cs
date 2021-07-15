using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : SingleTon<SystemManager> {

    //참조
    private EnemyManager enemy_manager;

    //상태
    public enum GameState
    {
        Idle,
        InGame,
        End
    }
    public GameState game_state = GameState.Idle;

    //초기화
    private void Start()
    {
        enemy_manager = EnemyManager.Instance;
    }

    private void Update()
    {
        //R키 누르면 게임 시작
        if (Input.GetKeyDown(KeyCode.R) && game_state == GameState.Idle)
        {
            GameStart(0);
        }

        //결과 화면에서 돌아가기
        if (Input.GetKeyDown(KeyCode.Space))
            BackToMenu();
    }

    //게임 시작
    public void GameStart(int _stage)
    {
        enemy_manager.GameStart(_stage);
        game_state = GameState.InGame;
    }

    //메뉴로 돌아가기
    public void BackToMenu()
    {
        game_state = GameState.Idle;
    }
}
