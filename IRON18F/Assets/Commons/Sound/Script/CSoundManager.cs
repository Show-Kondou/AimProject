using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//!  CSoundManager.cs	(シングルトン)
/*!
 * \details CSoundManager	サウンドをいろいろ制御
 * \author  Shoki Tsuneyama
 * \date    2016/10/21	新規作成

 */
public class CSoundManager : MonoBehaviour
{
    static GameObject m_gameObject = new GameObject("SoundManager");
    static protected CSoundManager m_instance = null;      // 中身
    public static CSoundManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = m_gameObject.AddComponent<CSoundManager>();

                //サウンドのリストを全ロード
                LoadSound();
                CreateChannel();
                DontDestroyOnLoad(m_gameObject);
            }
            return m_instance;
        }
    }

    #region Member

    //enumと連動させてファイルネーム取得する
    static public string GetFileName(EAudioList value)
    {
        string[] values ={
                             "Sound/BGM/Bgm01",
                             "Sound/BGM/Bgm02",
                             "Sound/BGM/Bgm03",
                             "Sound/BGM/Bgm04",
                             "Sound/SE/SE01",
                             "Sound/SE/SE02",
                             "Sound/SE/SE03",
                             "Sound/SE/SE04",
                             "Sound/SE/SE05",
                             "Sound/SE/SE06",
                             "Sound/SE/SE07",
                             "Sound/SE/SE08",
                             "Sound/SE/SE09",
                             "Sound/SE/SE10",
                             "Sound/SE/SE11",
                             "Sound/SE/SE12",
                             "Sound/SE/SE13",
                             "Sound/SE/SE14",
                             "Sound/SE/SE15",
                             "Sound/SE/SE16",
                             "Sound/SE/SE17",
                             "Sound/SE/SE18",
                             "Sound/SE/SE19",
                             "Sound/SE/SE20",
                             "Sound/SE/SE21",
                             "Sound/SE/SE22",
                             "Sound/SE/SE23",
                             "Sound/SE/SE24",
                             "Sound/SE/SE25",
                             "Sound/SE/SE26",
                             "Sound/SE/SE27",
                             "Sound/SE/SE28",
                             null};

        return values[(int)value];
    }
    int m_nowBgmIndex = 0;
    static GameObject[] m_bgm = new GameObject[2];
    static List<AudioClip> m_audioClips = new List<AudioClip>();
    #endregion

    #region Init
    //サウンドのリストをロード
    static void LoadSound()
    {
        for (int i = 0; i < (int)EAudioList.AudioLast; i++)
        {
            m_audioClips.Add(new AudioClip());
            m_audioClips[i] = Resources.Load(GetFileName((EAudioList)i)) as AudioClip;
            if (m_audioClips[i] == null)
                Debug.LogError("SEロードできひんのある");
        }
    }
    static void CreateChannel()
    {
        //SEチャンネルの作成
        for (int i = 0; i < (int)ESEChannelList.Num; i++)
        {
            m_seChannel[i] = new GameObject("SEChannel" + i);
            m_seChannel[i].transform.parent = m_gameObject.transform;
            m_seChannel[i].AddComponent<AudioSource>();
            m_seChannel[i].AddComponent<CAudio>();
        }
        for (int i = 0; i < 2; i++)
        {
            m_bgm[i] = new GameObject("BGMChannel"+i);
            m_bgm[i].transform.parent = m_gameObject.transform;
            m_bgm[i].AddComponent<AudioSource>();
            m_bgm[i].AddComponent<CAudio>();
        }
    }
    public enum EFadeType
    {
        In,
        Out,
        InOut,
        Cross,
        None
    }
    #endregion

    #region BGM
    public void MuteBgm(bool isMute)
    {
        m_bgm[m_nowBgmIndex].GetComponent<CAudio>().Mute(isMute);
    }
    //ヴォリューム0~1
    public void ChangeVolBGM(float vol,float sec,bool isFade)
    {
        m_bgm[m_nowBgmIndex].GetComponent<CAudio>().SetVolume(sec, vol, isFade);
    }
    //0~1
    //      (0でノーマル速度)  (1で最速)
    public void TempoUpBGM(float speed, float sec, bool isFade)
    {
        speed = speed * 2 + 1;
        if (speed <= 1)
            speed = 1;
        if (speed >= 3)
            speed = 3;
        m_bgm[m_nowBgmIndex].GetComponent<CAudio>().SetPitch(sec, speed, isFade);
    }
    //0~1
    //      (0で止まる)   (1でノーマル速度)
    public void SlowBGM(float speed, float sec, bool isFade)
    {
        if (speed <= 0)
            speed = 0;
        if (speed >= 1)
            speed = 1;
        m_bgm[m_nowBgmIndex].GetComponent<CAudio>().SetPitch(sec, speed, isFade);
    }
    //0~1
    //      (0でとまる)  (1で最速巻き戻し)
    public void RewindBGM(float speed, float sec, bool isFade)
    {
        speed = -speed * 3; 
        if (speed <= -3)
            speed = -3;
        if (speed >= 0)
            speed = 0;
        m_bgm[m_nowBgmIndex].GetComponent<CAudio>().SetPitch(sec, speed, isFade);
    }
    //ノーマル速度にする
    public void NormalSpeedBGM(float sec, bool isFade)
    {
        m_bgm[m_nowBgmIndex].GetComponent<CAudio>().SetPitch(sec, 1, isFade);
    }
    //フェードインのあとにフェードアウトする
    IEnumerator FadeInOut(EAudioList audio, float sec)
    {
        m_bgm[m_nowBgmIndex ^ 1].GetComponent<CAudio>().Stop(true, sec/2);
        for (float timer = 0; timer < sec / 2; timer += Time.deltaTime)
            yield return 0;
        m_bgm[m_nowBgmIndex].GetComponent<CAudio>().Play(m_audioClips[(int)audio], true, sec/2,true);
    }


    public void StopBGM(bool isFade)
    {
        m_bgm[m_nowBgmIndex].GetComponent<CAudio>().Stop(isFade, 1);
    }
    public void PlayBGM(EAudioList audio, EFadeType type, float sec)
    {
        m_nowBgmIndex ^= 1;
        bool inFade = false;
        bool outFade = false;
        switch (type)
        {
            case EFadeType.In:
                inFade = true;
                break;
            case EFadeType.Out:
                outFade = true;
                break;
            case EFadeType.InOut:
                StartCoroutine(FadeInOut(audio, sec));
                return;
                break;
            case EFadeType.Cross:
                inFade = outFade = true;
                break;
        }
        m_bgm[m_nowBgmIndex ^ 1].GetComponent<CAudio>().Stop(outFade, sec);
        m_bgm[m_nowBgmIndex].GetComponent<CAudio>().Play(m_audioClips[(int)audio], inFade, sec,true);
    }
    #endregion

    #region SE
    public enum ESEChannelList
    {
        _1,
        _2,
        _3,
        _4,
        _5,
        _6,
        _7,
        _8,
        Num,
        None
    }
    static GameObject[] m_seChannel = new GameObject[(int)ESEChannelList.Num];
    
    //Channelがnoneだと制御しないゲームオブジェクトを作成させる。
    public void PlaySE(EAudioList audio, ESEChannelList channnel,bool isLoop)
    {
        switch(channnel)
        {
            case ESEChannelList._1:
            case ESEChannelList._2:
            case ESEChannelList._3:
            case ESEChannelList._4:
            case ESEChannelList._5:
            case ESEChannelList._6:
            case ESEChannelList._7:
            case ESEChannelList._8:
                m_seChannel[(int)channnel].GetComponent<CAudio>().Play(m_audioClips[(int)audio], false, 0,isLoop);
                m_seChannel[(int)channnel].GetComponent<CAudio>().SetVolume(0, 1, false);
                break;
        }
    }
    //ヴォリューム0~1
    public void ChangeVolSE(ESEChannelList channnel, float vol)
    {
        switch (channnel)
        {
            case ESEChannelList._1:
            case ESEChannelList._2:
            case ESEChannelList._3:
            case ESEChannelList._4:
            case ESEChannelList._5:
            case ESEChannelList._6:
            case ESEChannelList._7:
            case ESEChannelList._8:
                m_seChannel[(int)channnel].GetComponent<CAudio>().SetVolume(0, vol, true);
                break;
        }
    }
    //0~1
    //      (0でノーマル速度)  (1で最速)
    public void TempoUpSE(ESEChannelList channnel, float speed)
    {
        speed = speed * 2 + 1;
        if (speed <= 1)
            speed = 1;
        if (speed >= 3)
            speed = 3;
        SetPitchSE(channnel, speed);
    }
    //0~1
    //      (0で止まる)   (1でノーマル速度)
    public void SlowSE(ESEChannelList channnel, float speed)
    {
        if (speed <= 0)
            speed = 0;
        if (speed >= 1)
            speed = 1;
        SetPitchSE(channnel, speed);
    }
    //0~1
    //      (0でとまる)  (1で最速巻き戻し)
    public void RewindSE(ESEChannelList channnel, float speed)
    {
        speed = -speed * 3;
        if (speed <= -3)
            speed = -3;
        if (speed >= 0)
            speed = 0;
        SetPitchSE(channnel, speed);
    }
    //ノーマル速度にする
    public void NormalSpeedSE(ESEChannelList channnel)
    {
        SetPitchSE(channnel, 1);
    }
    void SetPitchSE(ESEChannelList channnel,float speed)
    {
        switch (channnel)
        {
            case ESEChannelList._1:
            case ESEChannelList._2:
            case ESEChannelList._3:
            case ESEChannelList._4:
            case ESEChannelList._5:
            case ESEChannelList._6:
            case ESEChannelList._7:
            case ESEChannelList._8:
                m_seChannel[(int)channnel].GetComponent<CAudio>().SetPitch(0, speed, false);
                break;
        }
    }
    public void StopSE(ESEChannelList channnel)
    {
        m_seChannel[(int)channnel].GetComponent<CAudio>().Stop( false,0);
    }
    public bool CheckIsPlay(ESEChannelList channnel)
    {
        return m_seChannel[(int)channnel].GetComponent<CAudio>().GetIsPlaying();
    }
    public void PlaySE(EAudioList audio)
    {
        
        GameObject newSE = new GameObject("SE ");

        newSE.AddComponent<CAudio>();
        newSE.transform.parent = transform;
        newSE.GetComponent<CAudio>().PlayAndDestroy(m_audioClips[(int)audio]);
    }
    #endregion
}