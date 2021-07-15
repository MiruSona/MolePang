using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Define;

public class DataManager : SingleTon<DataManager> {
    
    //참조
    private SystemManager system_manager;

    //체력
    private float hp = 100f;                //현재 HP
    private const float hp_max = 100f;          //최대 HP
    private const float hp_dec_time = 2.0f;     //시간에 따라 감소하는 hp 양
    private Timer hp_sub_timer = new Timer(0, 0.50f);  //HP 감소 간격

    //스코어
    private const int score_max = 99999;    //스코어 최대치
    private int score = 0;                  //현재 스코어
    private int[] score_split;              //자릿수 별로 분리한 스코어
    private const int score_num = 5;        //자릿수

	//초기화
	private void Start () {
        system_manager = SystemManager.Instance;

        score_split = new int[score_num];
    }
	
	//업데이트
	void Update () {
        SystemManager.GameState state = system_manager.game_state;
        switch (state)
        {
            case SystemManager.GameState.Idle:
                //hp 초기화
                if(hp != hp_max)
                    hp = hp_max;

                //score 초기화
                if (score != 0)
                    score = 0;
                break;

            case SystemManager.GameState.InGame:
                //스코어 분리
                SplitScore();

                //일정 시간마다 hp 감소
                if (hp_sub_timer.AutoTimer())
                    SubHP(hp_dec_time);
                break;

            case SystemManager.GameState.End:

                break;
        }
	}
    
    #region HP관련
    public void SubHP(float _value)
    {
        if (hp - _value > 0)
            hp -= _value;
        else
        {
            hp = 0f;
            system_manager.game_state = SystemManager.GameState.End;
        }
    }

    //HP 증가
    public void AddHP(float _value)
    {
        if (hp + _value < hp_max)
            hp += _value;
        else
            hp = hp_max;
    }

    //HP 퍼센트
    public float GetHPPercent()
    {
        return hp / hp_max;
    }
    #endregion

    //스코어
    public void AddScore(int _value)
    {
        if (score + _value < score_max)
            score += _value;
        else
            score = score_max;
    }

    //스코어 자릿수 별로 분리
    private void SplitScore()
    {
        //스트링으로 변환
        string score_string = "" + score;
        //분리
        char[] num_char = score_string.ToCharArray();

        //0으로 초기화
        for (int i = 0; i < score_split.Length - num_char.Length; i++)
            score_split[i] = 0;

        for (int i = 0; i < num_char.Length; i++)
        {
            //역순으로 넣기
            int index = num_char.Length - i - 1;
            //char -> int로 변환
            score_split[index] = int.Parse(num_char[i].ToString());
        }
    }

    //Score 반환
    public int[] GetScoreSplit()
    {
        return score_split;
    }
}
