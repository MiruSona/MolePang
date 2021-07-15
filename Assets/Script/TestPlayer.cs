using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

    public float limit_height = 1.3f;
    public float init_height = 2.0f;
    public float speed = 0.02f;

    public Transform[] btns;

    //초기화
    void Start()
    {

    }

    //업데이트
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
            MoveDown(4);
        else
            MoveUp(4);

        if (Input.GetKey(KeyCode.S))
            MoveDown(2);
        else
            MoveUp(2);

        if (Input.GetKey(KeyCode.D))
            MoveDown(0);
        else
            MoveUp(0);

        if (Input.GetKey(KeyCode.F))
            MoveDown(1);
        else
            MoveUp(1);

        if (Input.GetKey(KeyCode.G))
            MoveDown(3);
        else
            MoveUp(3);
    }

    private void MoveDown(int _index)
    {
        Vector3 pos = btns[_index].position;
        if (pos.y > limit_height)
            btns[_index].Translate(Vector3.down * speed);
        else
            btns[_index].position = new Vector3(pos.x, limit_height, pos.z);
    }

    private void MoveUp(int _index)
    {
        Vector3 pos = btns[_index].position;
        if (pos.y < init_height)
            btns[_index].Translate(Vector3.up * speed * 2f);
        else
            btns[_index].position = new Vector3(pos.x, init_height, pos.z);
    }
}
