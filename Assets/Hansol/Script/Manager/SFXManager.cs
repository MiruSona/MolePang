using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class SFXManager : SingleTon<SFXManager> {

    //참조
    private SystemManager system_manager;
    private AudioSource sfx_effect;

    //쿨타임
    private bool spawn_cool = true;
    private bool hide_cool = true;
    private Timer spawn_cool_timer = new Timer(0, 0.1f);
    private Timer hide_cool_timer = new Timer(0, 0.1f);

    //사운드
    private const string sound_path = "Sound/FX/";
    private AudioClip[] fx_hit = new AudioClip[3];
    private AudioClip fx_explosion;
    private AudioClip fx_hide, fx_spawn;

    //목록
    public enum SFXList
    {
        Hit,
        Hide,
        Spawn,
        Explosion
    }

    //초기화
    void Start () {
        system_manager = SystemManager.Instance;
        sfx_effect = Resources.Load<AudioSource>("Prefab/SFXEffect");

        for (int i = 0; i < fx_hit.Length; i++)
            fx_hit[i] = Resources.Load<AudioClip>(sound_path + "FX_Hit_" + (i + 1));
        fx_explosion = Resources.Load<AudioClip>(sound_path + "FX_Bomb_Explosion");
        fx_hide = Resources.Load<AudioClip>(sound_path + "FX_Hide");
        fx_spawn = Resources.Load<AudioClip>(sound_path + "FX_Spawn");
    }

    //쿨타임
    private void Update()
    {
        if (!spawn_cool)
        {
            if (spawn_cool_timer.AutoTimer())
                spawn_cool = true;
        }

        if (!hide_cool)
        {
            if (hide_cool_timer.AutoTimer())
                hide_cool = true;
        }
    }

    //사운드
    public void PlaySFX(SFXList _select, float _volume)
    {
        if (system_manager.game_state != SystemManager.GameState.InGame)
            return;

        AudioSource sfx = Instantiate(sfx_effect);
        switch (_select)
        {
            //Hit은 랜덤 재생
            case SFXList.Hit:
                int index = Random.Range(0, fx_hit.Length);
                
                sfx.volume = _volume;
                sfx.clip = fx_hit[index];
                break;
            //여러개 트는거 방지 위해 쿨타임
            case SFXList.Hide:
                if (hide_cool)
                {
                    sfx.volume = _volume;
                    sfx.clip = fx_hide;
                    hide_cool = false;
                }
                break;
            //여러개 트는거 방지 위해 쿨타임
            case SFXList.Spawn:
                if (spawn_cool)
                {
                    sfx.volume = _volume;
                    sfx.clip = fx_spawn;
                    spawn_cool = false;
                }
                break;
            case SFXList.Explosion:
                sfx.volume = _volume;
                sfx.clip = fx_explosion;
                break;
        }
        sfx.Play();
    }
}
