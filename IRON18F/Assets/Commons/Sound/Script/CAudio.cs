using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CAudio : CAudioCliper
{
    class SAudioStatus
    {
        public byte m_priority =128;
        public float m_volume =0.1f;
        public float m_pitch =1;
        public float m_stereoPan =0;
        public float m_spatialBlend = 0;
        public float m_reverbZoneMin =1;
    }

    enum ECoroutine
    {
        FadeIn,
        FadeOut,
        Volume,
        Pitch,
        Last
    };

    SAudioStatus m_now = new SAudioStatus();
    SAudioStatus[] m_change = new SAudioStatus[2];
    CLeapCoroutine m_coroutine = new CLeapCoroutine();
    FEase m_ease = CEase.CUBIC;
    //ミュート設定
    // Use this for initialization
    void Start()
    {
        CreatecCroutine();
        InitStruct();
	}
    void CreatecCroutine()
    {
        if (m_coroutine == null)
        {
            m_coroutine = gameObject.AddComponent<CLeapCoroutine>();
            m_coroutine.Add(FadeIn, (int)ECoroutine.FadeIn);
            m_coroutine.Add(FadeOut, (int)ECoroutine.FadeOut);
            m_coroutine.Add(FadeVol, (int)ECoroutine.Volume);
            m_coroutine.Add(FadePitch, (int)ECoroutine.Pitch);
            m_ease = CEase.GetEasingFunction(EEaseType.Spring);
        }
    }
    void InitStruct()
    {
        if (m_change[0] == null || m_change[1] == null)
        {
            m_change[0] = new SAudioStatus();
            m_change[1] = new SAudioStatus();
        }
        if (m_now == null)
            m_now = new SAudioStatus();
    }
    public void Mute(bool isMute)
    {
        m_audio.mute = isMute;
    }
    public void SetVolume(float sec, float vol,bool isFade)
    {
        InitStruct();
        CreatSource();
        Mute(false);

        m_change[0].m_volume = m_now.m_volume;
        m_change[1].m_volume = vol;
        CreatecCroutine();
        if (isFade)
            m_coroutine.StartLeap((int)ECoroutine.Volume, sec, true);
        else
            m_audio.volume = m_now.m_volume = vol;
    }
    public void SetPitch(float sec, float pitch,bool isFade)
    {
        InitStruct();
        CreatSource();
        Mute(false);
        m_change[0].m_pitch = m_now.m_pitch;
        m_change[1].m_pitch = pitch;
        CreatecCroutine();
        if (isFade)
            m_coroutine.StartLeap((int)ECoroutine.Pitch, sec, true);
        else
            m_audio.pitch = m_now.m_pitch = pitch;
    }
    public void Stop(bool isFade ,float sec)
    {
        CreatSource();
        CreatecCroutine();
        if(isFade)
            m_coroutine.StartLeap((int)ECoroutine.FadeOut, sec, true);
        else
            m_audio.Stop();
    }
    public bool GetIsPlaying()
    {
        CreatSource();
        return m_audio.isPlaying;
    }
    public void Play(AudioClip clip,bool isFade ,float sec,bool isLoop)
    {
        InitStruct();
        CreatSource();
        m_audio.clip = clip;
        m_audio.loop = isLoop;
        m_audio.Play();
        Mute(false);
        m_now = new SAudioStatus();
        CreatecCroutine();
        if (isFade)
            m_coroutine.StartLeap((int)ECoroutine.FadeIn, sec, true);
        else
            m_audio.volume = m_now.m_volume;
    }
    public void PlayAndDestroy(AudioClip clip)
    {
        Play(clip, false, 0,false);
        Destroy(gameObject ,m_audio.clip.length);
    }
    #region CotoutineEvent
    //フェード系コルーチンイベント
    void FadeOut(float timer)
    {
        m_audio.volume = m_ease(m_now.m_volume, 0, timer);
    }
    void FadeIn(float timer)
    {
        m_audio.volume = m_ease(0, m_now.m_volume, timer);
     }
    void FadeVol(float timer)
    {
        m_audio.volume = m_now.m_volume = m_ease(m_change[0].m_volume, m_change[1].m_volume, timer);
    }
    void FadePitch(float timer)
    {
        m_audio.pitch = m_now.m_pitch = m_ease(m_change[0].m_pitch, m_change[1].m_pitch, timer);
    }
    #endregion	
}
