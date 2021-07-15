using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class MoleManager : SingleTon<MoleManager> {

    //참조
    private Mole[] moles;

    //상태
    public enum GameState
    {
        Start,
        InGame,
        End
    }
    private GameState game_state = GameState.Start;

    //스테이지
    private int stage = 1;
    private const int stage_unit = 5;

    //두더지 수
    private int mole_num = 0;
    private const int start_num = 3;

    //대기시간
    private Timer delay_timer = new Timer(0, 1.5f);

	//초기화
	void Start () {
        //참조
        moles = GetComponentsInChildren<Mole>();
        //미리 꺼둠
        foreach (Mole m in moles)
            m.gameObject.SetActive(false);
    }
	
	//업데이트
	void Update () {
        

        //게임 상태
        switch (game_state)
        {
            case GameState.Start:
                if (Input.GetKeyDown(KeyCode.R))
                    GameStart();
                break;

            case GameState.InGame:
                //모든 Mole 끝났나 체크
                if (CheckAllMolesDone())
                {
                    //딜레이
                    if (delay_timer.AutoTimer())
                    {

                        stage++;
                        NextStage();
                    }
                }
                break;
        }
        
	}

    //게임 시작
    private void GameStart()
    {
        //Mole 소환
        mole_num = start_num;
        for (int i = 0; i < mole_num; i++)
        {
            moles[i].Init();
            moles[i].gameObject.SetActive(true);
        }

        //상태 변경
        game_state = GameState.InGame;
    }

    //모든 mole들이 들어갔나 체크
    private bool CheckAllMolesDone()
    {
        bool done = true;
        //하나라도 켜져있으면 false
        foreach (Mole m in moles)
            if (m.gameObject.activeSelf)
                done = false;

        return done;
    }

    //다음 스테이지로
    private void NextStage()
    {
        //x스테이지마다 mole 숫자 추가 (단, 배열 길이 안넘게)
        if (stage % stage_unit == 0 && mole_num < moles.Length)
            mole_num++;

        //mole_num 만큼 소환
        for (int i = 0; i < mole_num; i++)
        {
            moles[i].Init();
            moles[i].gameObject.SetActive(true);
        }
    }
}
