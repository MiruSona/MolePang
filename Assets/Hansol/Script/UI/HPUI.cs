using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour {

    //참조
    private DataManager data_manager;
    private SystemManager system_manager;

    //컴포넌트
    private Image hp_gauge;

	//초기화
	void Start () {
        data_manager = DataManager.Instance;
        system_manager = SystemManager.Instance;
        hp_gauge = transform.GetChild(0).GetComponent<Image>();
    }
	
	//갱신
	void Update () {
        SystemManager.GameState state = system_manager.game_state;
        switch (state)
        {
            case SystemManager.GameState.End:
            case SystemManager.GameState.InGame:
                hp_gauge.fillAmount = data_manager.GetHPPercent();
                break;
        }
	}
}
