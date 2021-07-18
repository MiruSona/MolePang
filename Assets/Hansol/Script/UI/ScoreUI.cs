using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    //참조
    private DataManager data_manager;
    private SystemManager system_manager;

    //컴포넌트
    private Image[] scores;

    //숫자
    private Sprite[] num_sprites;

    //초기화
    void Start()
    {
        data_manager = DataManager.Instance;
        system_manager = SystemManager.Instance;
        scores = GetComponentsInChildren<Image>();
        num_sprites = Resources.LoadAll<Sprite>("Texture/UI/Number");
    }

    //갱신
    void Update()
    {
        SystemManager.GameState state = system_manager.game_state;
        switch (state)
        {
            case SystemManager.GameState.End:
            case SystemManager.GameState.InGame:
                //분리한 숫자 받음
                int[] split_num = data_manager.GetScoreSplit();
                //스코어 대입
                for (int i = 0; i < scores.Length; i++)
                    scores[i].sprite = num_sprites[split_num[i]];
                break;
        }
    }
}
