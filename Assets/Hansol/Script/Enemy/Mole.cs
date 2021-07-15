using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class Mole : EnemyBase {

    //참조
    private Texture face_normal, face_hit;

    //컴포넌트
    private MeshRenderer render;
        
    protected override void ChildInit()
    {
        //속성
        hp = 1;
        hp_max = 1;

        //타이머
        wait_timer = new Timer(0, 6f);

        //참조
        face_normal = Resources.Load<Texture>("Texture/Mole/MoleFaceNormal");
        face_hit = Resources.Load<Texture>("Texture/Mole/MoleFaceHit");
        die_effect = Resources.Load<GameObject>("Effect/DieEffect");

        //컴포넌트
        render = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    protected override void OptionalInit()
    {
        //표정 초기화
        if (render != null)
            render.material.mainTexture = face_normal;
    }

    protected override void Alive()
    {
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
}
