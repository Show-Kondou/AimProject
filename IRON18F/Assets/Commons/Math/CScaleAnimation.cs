//!  CScaleAnimation.cs
/*!
 * \details CScaleAnimation
 * \author  Shoki Tsuneyama
 * \date    2015/06/28　新規作成
 * \date    2015/10/14  機能縮小
 */

using UnityEngine;
using System.Collections;

public class CScaleAnimation : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    float m_beginScale = 1;

    [SerializeField]
    [Range(0, 10)]
    public float m_endScale = 1;

    [SerializeField]
    [Range(0.1f, 20)]
    public float m_endTime = 0;
    float m_timeCounter = 0;

    [SerializeField]
    bool m_isLooping = true;

    [SerializeField]
    bool m_isAwake = true;

    bool m_isDown = false;
    bool m_isPlay = false;

    void Start()
    {
        if (m_isAwake)
            m_isPlay = true;
    }

    void Update()
    {
        if (m_isPlay == false)
            return;

        m_timeCounter += Time.deltaTime;

        //タイムスケールが止まっているときはデフォルトタイムスケールを使用する
        if (m_endTime <= 0)
            m_endTime = 0.0001f;

        float t = m_timeCounter / m_endTime;

        if (m_isDown)
            transform.localScale = Vector3.one * Mathf.Lerp(m_beginScale, m_endScale, t);
        else
            transform.localScale = Vector3.one * Mathf.Lerp(m_endScale, m_beginScale, t);

        if (t >= 1.0f)
        {
            if (m_isLooping)
            {
                m_isDown ^= true;
                m_timeCounter = 0;
            }
        }

    }

    //! 非表示を解除して開始
    public void EneableRun()
    {
        if (enabled)
            return;
        enabled = true;
        m_timeCounter = 0;
        m_isDown = false;
        m_isPlay = true;
        transform.localScale = Vector3.one * m_beginScale;
    }

    //非表示
    public void Disible()
    {
        if (!enabled)
            return;
        enabled = false;
        m_timeCounter = 0;
        m_isDown = false;
        transform.localScale = Vector3.one * m_beginScale;
    }

    //一時停止
    public void Pause()
    {
        m_isPlay = false;
    }

    //停止
    public void Stop()
    {
        m_isPlay = false;
        m_timeCounter = 0;
    }

    //! 起動
    public void Run()
    {
        m_isPlay = true;
        m_timeCounter = 0;
        m_isDown = false;
        transform.localScale = Vector3.one * m_beginScale;
    }

}
