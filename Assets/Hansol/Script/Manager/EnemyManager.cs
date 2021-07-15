using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class EnemyManager : SingleTon<EnemyManager> {

    //참조
    private SystemManager system_manager;
    private Mole[] moles;
    private Bomb[] bombs;
    private MoleHelmat[] mole_hats;

    //스테이지
    private int stage = 1;
    private int stage_more_num = 5;         //5스테이지 마다 더 많은 수 등장
    private int stage_bomb = 5;             //폭탄 등장
    private int stage_mole_hat = 5;//15;    //모자쓴 두더지 등장
    private int stage_dec_time = 5;//20;   //시간 줄어듬

    //적군 소환 관련
    private Stack<int> rand_index = new Stack<int>();   //랜덤 인덱스 목록
    private int total_num = 0;          //소환 수
    private const int total_max = 5;    //최대 소환 수
    private const int start_num = 3;    //시작 소환 수

    //가중치
    private int mole_weight = 10;       //Mole 가중치
    private int mole_hat_weight = 5;   //MoleHat 가중치
    private int bomb_weight = 3;       //Bomb 가중치

    //최소 소환 수
    private int mole_min_num = 2;

    //시간관련
    private Timer delay_timer = new Timer(0, 1.5f); //대기 시간
    private const float dec_time = 1.0f;            //감소 시간

    //초기화
    void Start()
    {
        //참조
        system_manager = SystemManager.Instance;
        moles = GetComponentsInChildren<Mole>();
        bombs = GetComponentsInChildren<Bomb>();
        mole_hats = GetComponentsInChildren<MoleHelmat>();
        //미리 꺼둠
        foreach (Mole m in moles)
            m.gameObject.SetActive(false);
        foreach (Bomb b in bombs)
            b.gameObject.SetActive(false);
        foreach (MoleHelmat h in mole_hats)
            h.gameObject.SetActive(false);
    }

    //업데이트
    void Update()
    {
        //게임 상태
        SystemManager.GameState state = system_manager.game_state;
        switch (state)
        {
            case SystemManager.GameState.InGame:
                //모든 Mole 끝났나 체크
                if (CheckAllEnemiesDie())
                {
                    //딜레이
                    if (delay_timer.AutoTimer())
                    {
                        //스테이지++
                        stage++;

                        //unit 스테이지마다 작동 (단, 배열 길이 안넘게)
                        if (stage % stage_more_num == 0)
                        {
                            //최대 소환수++
                            if(total_num < total_max)
                                total_num++;

                            //시간 감소
                            if(stage >= stage_dec_time)
                                DecreaseWaitTime();
                        }
                        //소환
                        SummonEnemies();
                    }
                }
                break;

            case SystemManager.GameState.End:

                break;
        }
    }

    //게임 시작
    public void GameStart()
    {
        //적 소환
        total_num = start_num;
        SummonEnemies();
    }

    //모든 적들이 들어갔나 체크
    private bool CheckAllEnemiesDie()
    {
        bool done = true;
        //하나라도 켜져있으면 false
        foreach (Mole m in moles)
            if (m.gameObject.activeSelf)
                done = false;
        foreach (Bomb b in bombs)
            if (b.gameObject.activeSelf)
                done = false;
        foreach (MoleHelmat h in mole_hats)
            if (h.gameObject.activeSelf)
                done = false;

        return done;
    }

    //대기 시간 감소
    private void DecreaseWaitTime()
    {
        foreach (Mole m in moles)
            m.DecreaseWaitTime(dec_time);
        foreach (Bomb b in bombs)
            b.DecreaseWaitTime(dec_time);
        foreach (MoleHelmat h in mole_hats)
            h.DecreaseWaitTime(dec_time);
    }

    #region 적 소환
    //적 소환
    private void SummonEnemies()
    {
        //인덱스 램덤으로 구하기
        SetRandIndex(total_num);

        //정해진 수 소환 - 두더지 최소 2마리는 나오게
        for (int i = 0; i < mole_min_num; i++)
            SummonRandomMoleEnemy(rand_index.Pop());

        //랜덤 소환 - 트렙 포함
        foreach (int index in rand_index)
            SummonRandomAllEnemy(index);
    }

    //랜덤 인덱스 구하기
    private void SetRandIndex(int _num)
    {
        int rand_num = 0;
        //클리어
        rand_index.Clear();

        //정해진 숫자만큼 랜덤 숫자 넣기
        while (rand_index.ToArray().Length < _num)
        {
            //랜덤 숫자
            rand_num = Random.Range(0, total_max);

            //겹치는 숫자가 없으면 스택에 추가
            if (!rand_index.Contains(rand_num))
                rand_index.Push(rand_num);
        }
    }

    //랜덤 소환
    private void SummonRandomAllEnemy(int _index)
    {
        //랜덤 숫자
        int rand_num = 0;
        int total_weight = 0;
        int min = 0, max = 0;

        //그냥 두더지만 나옴
        total_weight = mole_weight;
        //분기점 - 폭탄 나옴
        if (stage >= stage_bomb)
            total_weight += bomb_weight;
        //분기점 - 모자쓴 두더지 나옴
        if (stage >= stage_mole_hat)
            total_weight += mole_hat_weight;

        //위에 기준으로 랜덤값
        rand_num = Random.Range(0, total_weight);

        //두더지 소환(0 <= x < mole_weight)
        min = 0;
        max = min + mole_weight;
        if (min <= rand_num && rand_num < max)
        {
            moles[_index].Init();
            moles[_index].gameObject.SetActive(true);
        }

        //폭탄 소환(mole_weight <= x < bomb_weight)
        min = max;
        max = min + bomb_weight;
        if (min <= rand_num && rand_num < max)
        {
            bombs[_index].Init();
            bombs[_index].gameObject.SetActive(true);
        }

        //모자쓴 두더지 소환(mole_weight <= x < mole_hat_weight)
        min = max;
        max = min + mole_hat_weight;
        if (min <= rand_num && rand_num < max)
        {
            mole_hats[_index].Init();
            mole_hats[_index].gameObject.SetActive(true);
        }
    }

    //두더지만 랜덤 소환
    private void SummonRandomMoleEnemy(int _index)
    {
        //랜덤 숫자
        int rand_num = 0;
        int total_weight = 0;
        int min = 0, max = 0;

        //그냥 두더지만 나옴
        total_weight = mole_weight;
        //분기점 - 모자쓴 두더지 나옴
        if (stage >= stage_mole_hat)
            total_weight += mole_hat_weight;
        //위에 기준으로 랜덤값
        rand_num = Random.Range(0, total_weight);

        //두더지 소환(0 <= x < mole_weight)
        min = 0;
        max = min + mole_weight;
        if (min <= rand_num && rand_num < max)
        {
            moles[_index].Init();
            moles[_index].gameObject.SetActive(true);
        }

        //모자쓴 두더지 소환(mole_weight <= x < mole_hat_weight)
        min = max;
        max = min + mole_hat_weight;
        if (min <= rand_num && rand_num < max)
        {
            mole_hats[_index].Init();
            mole_hats[_index].gameObject.SetActive(true);
        }
    } 
    #endregion
}
