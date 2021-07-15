using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Define;

public class UIButtons : MonoBehaviour {

    //참조
    private SystemManager system_manager;
    private UIManager ui_manager;

    //컴포넌트
    private Image timer_bar;

    //타이머
    public float ConfirmTime = 1.0f;
    private Timer delay_timer = null;

    //버튼 종류
    public enum Kind
    {
        GameStart,
        BackToMenu
    }
    public Kind kind = Kind.GameStart;

    //초기화
    private void Awake()
    {
        delay_timer = new Timer(0, ConfirmTime);
        timer_bar = transform.GetChild(0).GetComponent<Image>();
    }

    //켜질때 마다 초기화
    private void OnEnable()
    {
        timer_bar.fillAmount = 0f;
        delay_timer.InitTimer();
    }

    //초기화
    void Start()
    {
        system_manager = SystemManager.Instance;
        ui_manager = UIManager.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        //플레이어 확인
        if (other.CompareTag("Player"))
        {
            //타이머 표시
            timer_bar.fillAmount = delay_timer.time / delay_timer.limit;
            //딜레이 다되면 기능 실행
            if (delay_timer.AutoTimer())
                DoingFunction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //플레이어 확인
        if (other.CompareTag("Player"))
        {
            //타이머 초기화
            delay_timer.InitTimer();
            timer_bar.fillAmount = 0f;
        }
    }

    //종류에 따라 기능 실행
    private void DoingFunction()
    {
        switch (kind)
        {
            case Kind.GameStart: ui_manager.DifficultBtnsOn(); break;
            case Kind.BackToMenu: system_manager.BackToMenu(); break;
        }
    }
}
