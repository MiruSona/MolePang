using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class Mole : MonoBehaviour {

    //참조
    private Texture face_normal, face_hit;
    private GameObject explosion_effect;

    //컴포넌트
    private MeshRenderer render;

    //속성
    private int hp = 1;
    private const int hp_max = 1;
    private float move_speed = 0.03f;
    private enum State
    {
        GoUp, Alive, GoBack, Die
    }
    private State state = State.GoUp;

    //체크
    private bool attacked = false;
    private const float hit_height = -0.25f;
    private const float init_height = -1f;
    private const float limit_height = 0f;

    //시간 체크
    private Timer wait_timer = new Timer(0, 5f);
    
    //초기화
    public void Init()
    {
        hp = hp_max;
        attacked = false;
        Vector3 pos = transform.position;
        pos.y = init_height;
        transform.position = pos;
        wait_timer.time = 0f;
        if(render != null)
            render.material.mainTexture = face_normal;
        state = State.GoUp;
    }

    //초기화
    void Start () {
        //참조
        face_normal = Resources.Load<Texture>("Texture/Mole/MoleFaceNormal");
        face_hit = Resources.Load<Texture>("Texture/Mole/MoleFaceHit");
        explosion_effect = Resources.Load<GameObject>("Effect/ExplosionEffect");

        //컴포넌트
        render = transform.GetChild(0).GetComponent<MeshRenderer>();

        //기본 초기화
        Init();
    }
	
	//업데이트
	void FixedUpdate () {
        switch (state)
        {
            //생성 시 / 피격 후
            case State.GoUp:
                //올라가기(다올라가면 살아있다 표시)
                if (MoveUp())
                    state = State.Alive;
                break;

            //보통
            case State.Alive:
                //위치값
                Vector3 pos = transform.position;

                //시간 체크
                if (!wait_timer.CheckTimer())
                    wait_timer.AddTimer();
                //다되면 되돌아감
                else
                    state = State.GoBack;

                //피격 체크
                if (attacked)
                {
                    //얼굴 바꾸기
                    render.material.mainTexture = face_hit;
                    //높이 체크
                    if(pos.y <= hit_height)
                        SubHp();
                }
                //안맞으면 다시 평소 표정
                else
                    render.material.mainTexture = face_normal;

                //공격 당하는 중 아니면 올라가기
                if (!attacked)
                    MoveUp();
                break;

            //되돌아가기
            case State.GoBack:
                //내려가기(다내려가면 false)
                if (MoveDown())
                    gameObject.SetActive(false);
                break;

            //죽으면
            case State.Die:
                //내려가기(다내려가면 false)
                if (MoveDown())
                    gameObject.SetActive(false);
                break;
        }
	}

    //충돌 체크
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            attacked = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            attacked = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            attacked = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            attacked = false;
    }

    //올라가기
    private bool MoveUp()
    {
        //다 올라갔나 체크
        bool send_bool = false;
        //위치값
        Vector3 pos = transform.position;
        //Offset
        float offset = 0.01f;

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
    private bool MoveDown()
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
            send_bool = true;

        return send_bool;
    }

    //체력 관련
    private void SubHp()
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
            state = State.Die;
            //이펙트
            GameObject effect = Instantiate(explosion_effect);
            Vector3 pos = transform.position;
            pos.y = 0f;
            effect.transform.position = pos;
        }
        //아니면 다시 올라감
        else
            state = State.GoUp;
    }
}
