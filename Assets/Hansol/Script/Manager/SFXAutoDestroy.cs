using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAutoDestroy : MonoBehaviour {

    //컴포넌트
    private AudioSource sound;

	//초기화
	void Start () {
        sound = GetComponent<AudioSource>();
    }
	
	//체크
	void Update () {
        if (!sound.isPlaying)
            Destroy(gameObject);
	}
}
