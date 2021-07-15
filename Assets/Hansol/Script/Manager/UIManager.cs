using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleTon<UIManager> {

    //참조
    private SystemManager system_manager;
    private GameObject difficult_btns;
    private GameObject start_btn;

    //UI
    private Canvas title_ui, ingame_ui, end_ui;

	//초기화
	void Start () {
        //참조
        system_manager = SystemManager.Instance;
        difficult_btns = GameObject.Find("DifficultBtns");
        start_btn = GameObject.Find("StartBtn");
        DifficultBtnsOff();

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

    //난이도 버튼 On
    public void DifficultBtnsOn()
    {
        difficult_btns.SetActive(true);
        start_btn.SetActive(false);
    }

    //난이도 버튼 Off
    public void DifficultBtnsOff()
    {
        difficult_btns.SetActive(false);
        start_btn.SetActive(true);
    }
}
