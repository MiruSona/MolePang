using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public abstract class EnemyBase : MonoBehaviour {

    //참조
    protected GameObject die_effect = null;
    protected SFXManager sfx_manager = null;

    //속성
    protected int hp = 1;
    protected int hp_max = 1;
    protected float move_speed = 0.03f;
    protected enum State
    {
        GoUp, Alive, GoBack, Die
    }
    protected State state = State.GoUp;

    //체크
    protected bool attacked = false;
    protected float hit_height = -0.25f;
    protected float init_height = -1f;
    protected float limit_height = 0f;

    //시간 체크
    protected Timer wait_timer = new Timer(0, 6f);
    protected float limit_time_min = 2f;

    //사운드
    protected float spawn_volume = 1.0f;
    protected float die_volume = 1.0f;
    protected float hide_volume = 1.0f;
    protected bool play_spawn_sound = false;
    protected SFXManager.SFXList die_sound = SFXManager.SFXList.Hit;

    //초기화
    public void Init()
    {
        //속성 초기화
        hp = hp_max;
        attacked = false;
        //위치 이동
        Vector3 pos = transform.position;
        pos.y = init_height;
        transform.position = pos;
        //타이머 초기화
        wait_timer.time = 0f;
        //스폰 사운드 초기화
        play_spawn_sound = false;
        //상태 초기화
        state = State.GoUp;

        OptionalInit();
    }

    //자식 초기화
    protected abstract void ChildInit();

    //외부 초기화(옵션)
    protected abstract void OptionalInit();

    //초기화
    void Start()
    {
        //필수
        sfx_manager = SFXManager.Instance;

        //기본 초기화
        Init();

        //자식 초기화
        ChildInit();
    }

    //업데이트
    void FixedUpdate()
    {
        //상태 처리
        switch (state)
        {
            //생성 시 / 피격 후
            case State.GoUp:
                GoUp();
                break;

            //보통
            case State.Alive:
                Alive();
                break;

            //되돌아가기
            case State.GoBack:
                GoBack();
                break;

            //죽으면
            case State.Die:
                Die();
                break;
        }
    }
    
    #region 상태 관련
    protected virtual void GoUp()
    {
        //올라가기(다올라가면 살아있다 표시)
        if (MoveUp())
            state = State.Alive;

        //누르고 기다리는거 방지
        //시간 체크
        if (!wait_timer.CheckTimer())
            wait_timer.AddTimer();
        //다되면 되돌아감
        else
            state = State.GoBack;
    }

    protected abstract void Alive();

    protected virtual void GoBack()
    {
        //내려가기(다내려가면 false)
        if (MoveDown())
            gameObject.SetActive(false);
    }

    protected virtual void Die()
    {
        //바로 false
        gameObject.SetActive(false);
    }
    #endregion

    #region 충돌 체크
    protected void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            attacked = true;
    }
    protected void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            attacked = false;
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            attacked = true;
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            attacked = false;
    }
    #endregion

    #region 움직임
    //올라가기
    protected bool MoveUp()
    {
        //다 올라갔나 체크
        bool send_bool = false;
        //위치값
        Vector3 pos = transform.position;
        //Offset
        float offset = 0.01f;

        //사운드
        if (!play_spawn_sound)
        {
            sfx_manager.PlaySFX(SFXManager.SFXList.Spawn, spawn_volume);
            play_spawn_sound = true;
        }

        //올라가기
        if (pos.y < limit_height - offset)
            transform.Translate(0, move_speed, 0, Space.World);
        //다올라가면 limit_height로 설정
        else
        {
            pos.y = limit_height;
            transform.position = pos;
            send_bool = true;
        }

        return send_bool;
    }

    //내려가기
    protected bool MoveDown()
    {
        //다 내려갔나 체크
        bool send_bool = false;
        //위치값
        Vector3 pos = transform.position;
        //Offset
        float offset = 0.01f;

        //올라가기
        if (pos.y > init_height + offset)
            transform.Translate(0, -move_speed, 0, Space.World);
        //다내려가면
        else
        {
            send_bool = true;
            //사운드
            sfx_manager.PlaySFX(SFXManager.SFXList.Hide, hide_volume);
        }

        return send_bool;
    } 
    #endregion

    //체력--
    protected virtual void SubHp()
    {
        if (state != State.Alive)
            return;

        if (hp > 0)
            hp--;
        else
            hp = 0;

        //0이면 죽음
        if (hp == 0)
        {
            //이펙트
            GameObject effect = Instantiate(die_effect);
            Vector3 pos = transform.position;
            pos.y = 0f;
            effect.transform.position = pos;
            //사운드
            sfx_manager.PlaySFX(die_sound, die_volume);
            //상태
            state = State.Die;
        }
    }

    //시간--
    public void DecreaseWaitTime(float _sub_time)
    {
        //최소시간 보다 많으면 뺌
        if (wait_timer.limit - _sub_time > limit_time_min)
            wait_timer.limit -= _sub_time;
        //아니면 최소시간으로
        else
            wait_timer.limit = limit_time_min;
    }
}
