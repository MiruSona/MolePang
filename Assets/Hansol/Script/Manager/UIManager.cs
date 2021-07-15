using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //참조
    private SystemManager system_manager;

    //UI
    private Canvas title_ui, ingame_ui, end_ui;

	//초기화
	void Start () {
        //참조
        system_manager = SystemManager.Instance;

        //UI 초기화
        Canvas[] uis = GetComponentsInChildren<Canvas>();
        foreach(Canvas c in uis)
        {
            switch (c.name)
            {
                case "Title": title_ui = c; break;
                case "InGame": ingame_ui = c; break;
                case "End": end_ui = c; break;
            }
        }
	}
	
	//게임 상태에 따라 UI On/Off
	void Update () {
        SystemManager.GameState state = system_manager.game_state;
        switch (state)
        {
            case SystemManager.GameState.Idle:
                title_ui.gameObject.SetActive(true);
                ingame_ui.gameObject.SetActive(false);
                end_ui.gameObject.SetActive(false);
                break;
            case SystemManager.GameState.InGame:
                title_ui.gameObject.SetActive(false);
                ingame_ui.gameObject.SetActive(true);
                end_ui.gameObject.SetActive(false);
                break;
            case SystemManager.GameState.End:
                title_ui.gameObject.SetActive(false);
                ingame_ui.gameObject.SetActive(false);
                end_ui.gameObject.SetActive(true);
                break;
        }
	}
}
