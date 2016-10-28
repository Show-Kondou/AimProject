//!  CLerp.cs
/*!
 * \details 指定位置から線形補完で移動してくるクラス
 * \author  Shoki Tsuneyama
 * \date    2015/07/02　新規作成
 * \date    2015/10/14  機能縮小
 */

using UnityEngine;
using System.Collections;

public class CLerp : MonoBehaviour
{
    [Range(0.01f, 200)]
    public float m_time = 0;
    public Vector2 m_beginPos;
    [Header("移動を滑らかにするかどうか")]
    public bool m_isCubic = true;
    [Header("座標指定はローカルかどうか")]
    public bool m_isLocal = true;
    [Header("オブジェクト生成時に自動開始するかどうか")]
    public bool m_isPlayOnAwake = true;

    [Header("初期座標をBeginPosにするかどうか")]
    public bool m_isInversPos = false;

    [Header("デバッグ用リセットボタン")]
    public bool m_isDebugReset = false;

    [Header("自動ループ")]
    public bool m_isLoop = false;

    [Header("ループ中の自動反転")]
    public bool m_isAutoNonInvers = true;

    [Header("親オブジェクトに追従させるか")]
    public bool m_isParent = false;

    [Header("非アクティブ時にリセットする？")]
    public bool m_isEnableReset = false;

    bool m_isPlay = true;
    bool m_isInvers = false;

    protected float m_startTime = 0;
    Vector3 m_to;
    Vector3 m_from;

    void OnEnable()
    {
        if (m_isEnableReset)
            Reset();
    }

    //初期化
    void Start()
    {
        m_startTime = 0;
        m_to = transform.position;
        m_from = m_beginPos;
        if (m_isLocal)
            m_from += m_to;
        m_isPlay = m_isPlayOnAwake;
        if (m_isParent)
        {
            m_to = transform.localPosition;
            m_from = m_beginPos;
            m_from += m_to;
        }

        if (m_isInversPos)
        {
            transform.position = m_from;
            m_isInvers = true;
        }

    }

    //! 更新
    void Update()
    {
        //起動中かどうかのチェック
        if (m_isPlay == false)
            return;

        //デバッグモードの有効化
        if (m_isDebugReset)
        {
            m_isDebugReset = false;
            m_from = m_beginPos;
            if (m_isLocal)
                m_from += m_to;
            m_startTime = 0;
        }

        //! 時間の更新
        m_startTime += Time.deltaTime;
        float t = Mathf.Clamp01(m_startTime / m_time);

        //! 補完計算
        if (m_isCubic)
            CMath.Cubic(t);

        //! 移動の逆転
        if (m_isInvers)
        {
            if (m_isParent)
                transform.localPosition = Vector3.Lerp(m_to, m_from, t);
            else
                transform.position = Vector3.Lerp(m_to, m_from, t);
        }
        else
        {
            if (m_isParent)
                transform.localPosition = Vector3.Lerp(m_from, m_to, t);
            else
                transform.position = Vector3.Lerp(m_from, m_to, t);
        }
        //! 自動繰り返しのON OFF
        if (m_isLoop && t >= 1.0f)
        {
            if (m_isAutoNonInvers)
                m_isInvers ^= true;
            m_startTime = 0;
        }
    }

    //! 移動終了かチェックする
    public bool IsEnd()
    {
        return m_startTime > m_time;
    }

    //! 起動中かどうかを取得
    public bool IsPlay()
    {
        if (IsEnd())
            return false;
        return m_isPlay;
    }

    //! 外部からの起動命令
    public void Run(bool isInvers = false)
    {
        m_isPlay = true;
        m_isInvers = isInvers;
    }

    //! 初期位置に移動
    public void ResetBeginPos()
    {
        if (m_isParent)
            transform.localPosition = m_from;
        else
            transform.position = m_from;
        m_startTime = 0;
    }

    //! タイマーを0にする
    public void Reset()
    {
        m_startTime = 0;
    }

    //! 一時停止
    public void Pause()
    {
        m_isPlay = false;
    }

    //停止
    public void Stop()
    {
        m_isPlay = false;
        m_startTime = 0;
    }
}
