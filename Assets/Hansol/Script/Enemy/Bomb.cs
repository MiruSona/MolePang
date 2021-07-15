using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class Bomb : EnemyBase {
    
    protected override void ChildInit()
    {
        //속성
        hp = 1;
        hp_max = 1;
        move_speed = 0.03f;

        //체크값
        hit_height = 0.2f;
        init_height = -0.7f;
        limit_height = 0.3f;

        //시간 체크
        wait_timer = new Timer(0, 6.5f);
        limit_time_min = 2.5f;

        //참조
        die_effect = Resources.Load<GameObject>("Effect/BombEffect");

        //사운드
        die_sound = SFXManager.SFXList.Explosion;
        hide_volume = 0f;
        die_volume = 0.5f;
    }

    protected override void OptionalInit()
    {

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
            //높이 체크
            if (pos.y <= hit_height)
                SubHp();
        }

        //공격 당하는 중 아니면 올라가기
        if (!attacked)
            MoveUp();
    }
}
