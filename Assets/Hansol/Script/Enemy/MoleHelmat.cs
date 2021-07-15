using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleHelmat : EnemyBase {

    //참조
    private Texture face_normal, face_hit;
    private GameObject hit_effect;

    //컴포넌트
    private MeshRenderer render;
    private Animator animator;
    
    protected override void ChildInit()
    {
        //속성
        hp = 2;
        hp_max = 2;
        move_speed = 0.03f;

        //체크값
        hit_height = -0.25f;
        init_height = -1f;
        limit_height = 0f;

        //타이머
        wait_timer = new Define.Timer(0, 7f);
        limit_time_min = 3f;

        //참조
        face_normal = Resources.Load<Texture>("Texture/Mole/MoleFaceNormal");
        face_hit = Resources.Load<Texture>("Texture/Mole/MoleFaceHit");
        die_effect = Resources.Load<GameObject>("Effect/DieEffect");
        hit_effect = Resources.Load<GameObject>("Effect/HitEffect");

        //컴포넌트
        render = transform.GetChild(0).GetComponent<MeshRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    protected override void OptionalInit()
    {
        //표정 초기화
        if (render != null)
            render.material.mainTexture = face_normal;

        //애니메이션 초기화
        if (animator != null)
        {
            animator.gameObject.SetActive(true);
            animator.Rebind();
        }
    }

    protected override void Alive()
    {
        //위치값
        Vector3 pos = transform.position;

        //애니메이션 다됬나 체크
        if (animator.gameObject.activeSelf)
        {
            AnimatorStateInfo anim_state = animator.GetCurrentAnimatorStateInfo(0);
            //다됬으면 끔
            if (anim_state.IsName("Mole_Helmet_Open") && anim_state.normalizedTime > 1)
                animator.gameObject.SetActive(false);
        }

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
            if (pos.y <= hit_height)
                SubHp();
        }
        //안맞으면 다시 평소 표정
        else
            render.material.mainTexture = face_normal;

        //공격 당하는 중 아니면 올라가기
        if (!attacked)
            MoveUp();
    }

    protected override void SubHp()
    {
        base.SubHp();

        //1이면 피격 애니메이션 + 이펙트
        if (hp == 1)
        {
            //이펙트
            GameObject effect = Instantiate(hit_effect);
            Vector3 pos = transform.position;
            pos.y = 0f;
            effect.transform.position = pos;
            //애니메이션
            animator.SetBool("Attacked", true);
            //사운드
            sfx_manager.PlaySFX(die_sound, die_volume);
            //상태 처리
            state = State.GoUp;
        }
    }
}
