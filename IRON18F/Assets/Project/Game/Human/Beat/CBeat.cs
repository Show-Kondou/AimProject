using UnityEngine;
using System.Collections;

public class CBeat 
{
    #region Private
    void Play()
    {
        if (CSoundManager.Instance.CheckIsPlay(CSoundManager.ESEChannelList._8)==false)
            CSoundManager.Instance.PlaySE(EAudioList.SE_HumanBeat, CSoundManager.ESEChannelList._8, true);
    }
    void Stop()
    {
        CSoundManager.Instance.StopSE(CSoundManager.ESEChannelList._8);
    }
    #endregion

    //  power = 0~1 
    //0で停止
    //1でMAX
    public void Panic(float power)
    {
        if (power <= 0.01)
            Stop();
        else
            Play();
        power = power /2 +0.5f;
        CSoundManager.Instance.SlowSE(CSoundManager.ESEChannelList._8, power);
    }
}