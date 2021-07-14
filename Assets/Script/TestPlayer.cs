using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour {

    public float limit_height = 1.3f;
    public float init_height = 2.0f;
    public float speed = 0.02f;

    public Transform test_cube;
    public Mole test_mole;

    //초기화
    void Start()
    {

    }

    //업데이트
    void FixedUpdate()
    {
        Vector3 pos = test_cube.position;
        if (Input.GetKey(KeyCode.A))
        {
            if (pos.y > limit_height)
                test_cube.Translate(Vector3.down * speed);
            else
                test_cube.position = new Vector3(pos.x, limit_height, pos.z);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            test_mole.Init();
            test_mole.gameObject.SetActive(true);
        }

        if (!Input.anyKey)
        {
            if (pos.y < init_height)
                test_cube.Translate(Vector3.up * speed * 2f);
            else
                test_cube.position = new Vector3(pos.x, init_height, pos.z);
        }
    }
}
